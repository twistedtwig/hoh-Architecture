using hoh.architecture.CQRS.Command;
using hoh.architecture.CQRS.Query;
using hoh.architecture.Shared.Configuration;
using Microsoft.Extensions.Options;

namespace hoh.architecture.CQRS.Logging
{
    public class EntityFrameworkCommandQueryLogger : ICommandQueryLogging
    {
        private readonly IOptions<HohArchitectureOptions> _hohOptions;
        public EntityFrameworkCommandQueryLogger(IOptions<HohArchitectureOptions> options)
        {
            _hohOptions = options;
        }

        public Task LogQueryAsync<T>(T query, QueryCommandLoggingResult result) where T : IQuery
        {
            throw new NotImplementedException();
        }

        public Task LogCommandAsync<T>(T command, QueryCommandLoggingResult result) where T : ICommand
        {
            throw new NotImplementedException();
        }
    }
}
