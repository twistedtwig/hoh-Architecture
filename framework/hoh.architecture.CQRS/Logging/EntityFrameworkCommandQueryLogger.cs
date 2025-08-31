using System.Text.Json;
using HoH.Architecture.CQRS.Command;
using HoH.Architecture.CQRS.Query;

namespace HoH.Architecture.CQRS.Logging
{
    public class EntityFrameworkCommandQueryLogger : ICommandQueryLogging
    {
        private LoggingDbContext DbContext { get; }
        public EntityFrameworkCommandQueryLogger(LoggingDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public async Task LogQueryAsync<T>(T query, QueryCommandLoggingResult result) where T : IQuery
        {
            await using var transaction = await DbContext.Database.BeginTransactionAsync();

            var json = string.Empty;
            var error = result.Error;
            try
            {
                json = JsonSerializer.Serialize(query);
            }
            catch (Exception e)
            {
                error += $" Unable to serialize Query {typeof(T).Name}, {e.Message}";
            }

            DbContext.Set<LoggingEntity>().Add(new LoggingEntity
            {
                Type = QueryCommandLoggingType.Query,
                ExecutionTime = result.ExecutionTime,
                TimeSpan = result.TimeSpan,
                Success = result.Success,
                Error = error,
                ItemJson = json,
            });

            await DbContext.SaveChangesAsync();
            await transaction.CommitAsync();
        }

        public Task LogCommandAsync<T>(T command, QueryCommandLoggingResult result) where T : ICommand
        {
            throw new NotImplementedException();
        }
    }
}
