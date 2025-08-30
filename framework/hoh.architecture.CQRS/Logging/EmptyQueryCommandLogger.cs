using hoh.architecture.CQRS.Command;
using hoh.architecture.CQRS.Query;

namespace hoh.architecture.CQRS.Logging
{
    public class EmptyQueryCommandLogger : IQueryCommandLogging
    {
        public Task LogQueryAsync<T>(T query, QueryCommandLoggingResult result) where T : IQuery
        {
            return Task.CompletedTask;
        }

        public Task LogCommandAsync<T>(T command, QueryCommandLoggingResult result) where T : ICommand
        {
            return Task.CompletedTask;
        }
    }
}
