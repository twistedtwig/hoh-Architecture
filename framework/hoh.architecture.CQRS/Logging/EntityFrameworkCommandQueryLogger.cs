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

            DbContext.Set<LoggingEntity>().Add(new LoggingEntity
            {
                ExecutionTime = result.ExecutionTime,
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
