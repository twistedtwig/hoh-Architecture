using HoH.Architecture.CQRS.Command;
using HoH.Architecture.CQRS.Query;
using System.Text.Json;

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
            await LogDataAsync(query, result, QueryCommandLoggingType.Query);

        }

        public async Task LogCommandAsync<T>(T command, QueryCommandLoggingResult result) where T : ICommand
        {
            await LogDataAsync(command, result, QueryCommandLoggingType.Command);
        }

        private async Task LogDataAsync<T>(T item, QueryCommandLoggingResult result, QueryCommandLoggingType type)
        {
            await using var transaction = await DbContext.Database.BeginTransactionAsync();

            var json = string.Empty;
            var error = result.Error;
            try
            {
                json = JsonSerializer.Serialize(item);
            }
            catch (Exception e)
            {
                error += $" Unable to serialize item {typeof(T).Name}, {e.Message}";
            }

            DbContext.Set<LoggingEntity>().Add(new LoggingEntity
            {
                Type = type,
                ExecutionTime = result.ExecutionTime,
                TimeSpan = result.TimeSpan,
                Success = result.Success,
                Error = error,
                ItemJson = json,
                HandlerType = result.HandlerType.FullName,
                ItemType = typeof(T).FullName,
            });

            await DbContext.SaveChangesAsync();
            await transaction.CommitAsync();
        }
    }
}
