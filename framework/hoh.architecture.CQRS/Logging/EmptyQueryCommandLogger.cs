using hoh.architecture.CQRS.Command;
using hoh.architecture.CQRS.Query;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace hoh.architecture.CQRS.Logging
{
    public class EmptyQueryCommandLogger : IQueryCommandLogging
    {
        public Task RegisterServiceAsync(IApplicationBuilder app, IServiceCollection serviceCollection)
        {
            return Task.CompletedTask;
        }

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
