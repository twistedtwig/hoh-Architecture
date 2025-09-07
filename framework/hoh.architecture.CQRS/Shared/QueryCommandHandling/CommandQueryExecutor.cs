using HoH.Architecture.CQRS.Command;
using HoH.Architecture.CQRS.ExceptionHandling;
using HoH.Architecture.CQRS.Logging;
using HoH.Architecture.CQRS.Query;
using HoH.Architecture.CQRS.Shared.Results;

namespace HoH.Architecture.CQRS.Shared.QueryCommandHandling
{
    public class CommandQueryExecutor : IQueryCommandExecutor
    {
        private readonly IQueryCommandLocator _queryCommandLocator;
        private readonly ICommandQueryLogging? _logging;
        private readonly IExceptionHandler? _exceptionHandler;

        public CommandQueryExecutor(IQueryCommandLocator queryCommandLocator) : this(queryCommandLocator, null, null)
        {
        }

        public CommandQueryExecutor(IQueryCommandLocator queryCommandLocator, ICommandQueryLogging? logging = null) : this(queryCommandLocator, logging, null)
        {
        }

        public CommandQueryExecutor(IQueryCommandLocator queryCommandLocator, ICommandQueryLogging? logging = null, IExceptionHandler? exceptionHandler = null)
        {
            _queryCommandLocator = queryCommandLocator;
            _logging = logging;
            _exceptionHandler = exceptionHandler;
        }

        public async Task<IQueryResult<TR>> ExecuteQueryAsync<TQ, TR>(TQ query) where TQ : IQuery where TR : class
        {
            var startTime = DateTime.Now.ToUniversalTime();
            var watch = System.Diagnostics.Stopwatch.StartNew();
            var error = string.Empty;
            Type queryHandlerType = null;
            IQueryResult<TR> result = null;

            try
            {
                if (query == null)
                {
                    throw new ArgumentNullException(nameof(query));
                }

                var queryHandler = await _queryCommandLocator.LocateQueryHandlerAsync<TQ, TR>();
                if (queryHandler == null)
                {
                    throw new ArgumentNullException(nameof(queryHandler));
                }

                queryHandlerType = queryHandler.GetType();
                result = await queryHandler.ExecuteAsync(query);
            }
            catch (Exception ex)
            {
                if (_exceptionHandler == null)
                {
                    throw;
                }

                var dto = await _exceptionHandler.HandleQueryExecutionExceptionAsync<TQ, TR>(ex, query);
                if (dto != null)
                {
                    if (dto.AllowExceptionToBubbleUp)
                    {
                        throw;
                    }

                    if (dto.ResultOverride != null)
                    {
                        result = dto.ResultOverride;
                    }
                    else
                    {
                        result = new QueryResult<TR>(false, null, new ExceptionalMessage(ex));
                    }
                }
                else
                {
                    throw;
                }
            }
            finally
            {
                watch.Stop();

                if (_logging != null)
                {
                    var loggingResult = new QueryCommandLoggingResult
                    {
                        Error = error,
                        ExecutionTime = startTime,
                        Success = result?.Success ?? false,
                        TimeSpan = watch.Elapsed,
                        HandlerType = queryHandlerType,
                    };

                    await _logging.LogQueryAsync(query, loggingResult);
                }
            }

            return result;
        }

        public async Task<ICommandResult> ExecuteCommandAsync<TC>(TC command) where TC : ICommand
        {
            var startTime = DateTime.Now.ToUniversalTime();
            var watch = System.Diagnostics.Stopwatch.StartNew();
            var error = string.Empty;
            Type commandHandlerType = null;
            ICommandResult? result = null;

            try
            {
                if (command == null)
                {
                    throw new ArgumentNullException(nameof(command));
                }

                var commandHandler = await _queryCommandLocator.LocateCommandHandlerAsync<TC>();
                if (commandHandler == null)
                {
                    throw new ArgumentNullException(nameof(commandHandler));
                }

                commandHandlerType = commandHandler.GetType();
                result = await commandHandler.ExecuteAsync(command);
            }
            catch (Exception ex)
            {
                if (_exceptionHandler == null)
                {
                    throw;
                }

                var dto = await _exceptionHandler.HandleCommandExecutionExceptionAsync<TC>(ex, command);
                if (dto != null)
                {
                    if (dto.AllowExceptionToBubbleUp)
                    {
                        throw;
                    }

                    result = dto.ResultOverride ?? new CommandResult(false, new ExceptionalMessage(ex));
                }
                else
                {
                    throw;
                }
            }
            finally
            {
                watch.Stop();

                if (_logging != null)
                {
                    var loggingResult = new QueryCommandLoggingResult
                    {
                        Error = error,
                        ExecutionTime = startTime,
                        Success = result?.Success ?? false,
                        TimeSpan = watch.Elapsed,
                        HandlerType = commandHandlerType.GetType(),
                    };

                    await _logging.LogCommandAsync(command, loggingResult);
                }
            }

            return result;
        }
    }
}
