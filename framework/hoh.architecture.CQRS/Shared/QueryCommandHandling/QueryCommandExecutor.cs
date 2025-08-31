using hoh.architecture.CQRS.Command;
using hoh.architecture.CQRS.Logging;
using hoh.architecture.CQRS.Query;
using hoh.architecture.CQRS.Shared.Results;

namespace hoh.architecture.CQRS.Shared.QueryCommandHandling
{
    public class QueryCommandExecutor : IQueryCommandExecutor
    {
        private readonly IQueryCommandLocator _queryCommandLocator;
        private readonly ICommandQueryLogging _logging;

        public QueryCommandExecutor(IQueryCommandLocator queryCommandLocator, ICommandQueryLogging logging)
        {
            _queryCommandLocator = queryCommandLocator;
            _logging = logging;
        }

        public async Task<IQueryResult<TR>> ExecuteAsync<TQ, TR>(TQ query) where TQ : IQuery where TR : class
        {
            var startTime = DateTime.Now.ToUniversalTime();
            var watch = System.Diagnostics.Stopwatch.StartNew();

            if (query == null)
            {
                throw new ArgumentNullException(nameof(query));
            }

            var queryHandler = await _queryCommandLocator.LocateQueryHandlerAsync<TQ, TR>();
            if (queryHandler == null)
            {
                throw new ArgumentNullException(nameof(queryHandler));
            }

            var error = string.Empty;
            var success = true;
            IQueryResult<TR> result;
            try
            {
                result = await queryHandler.ExecuteAsync(query);

            }
            catch (Exception ex)
            {
                error = ex.Message;
                success = false;
                throw;
            }
            finally
            {
                watch.Stop();

                var loggingResult = new QueryCommandLoggingResult
                {
                    Error = error,
                    ExecutionTime = startTime,
                    Success = success,
                    TimeSpan = watch.Elapsed
                };

                await _logging.LogQueryAsync(query, loggingResult);
            }

            return result;
        }

        public Task<ICommandResult> ExecuteAsync<TC>(TC command) where TC : ICommand
        {
            throw new NotImplementedException();
        }
    }
}
