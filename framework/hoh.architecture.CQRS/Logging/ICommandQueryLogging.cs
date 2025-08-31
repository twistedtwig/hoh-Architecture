using hoh.architecture.CQRS.Command;
using hoh.architecture.CQRS.Query;

namespace hoh.architecture.CQRS.Logging
{
    public interface ICommandQueryLogging
    {
        public Task LogQueryAsync<T>(T query, QueryCommandLoggingResult result) where T : IQuery;
        public Task LogCommandAsync<T>(T command, QueryCommandLoggingResult result) where T : ICommand;
    }
}
