using HoH.Architecture.CQRS.Command;
using HoH.Architecture.CQRS.Query;

namespace HoH.Architecture.CQRS.Logging
{
    public interface ICommandQueryLogging
    {
        public Task LogQueryAsync<T>(T query, QueryCommandLoggingResult result) where T : IQuery;
        public Task LogCommandAsync<T>(T command, QueryCommandLoggingResult result) where T : ICommand;
    }
}
