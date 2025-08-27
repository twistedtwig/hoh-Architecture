using hoh.architecture.CQRS.Command;
using hoh.architecture.CQRS.Query;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace hoh.architecture.CQRS.Logging
{
    public interface IQueryCommandLogging
    {
        public Task RegisterServiceAsync(IApplicationBuilder app, IServiceCollection serviceCollection);
        public Task LogQueryAsync<T>(T query, QueryCommandLoggingResult result) where T : IQuery;
        public Task LogCommandAsync<T>(T command, QueryCommandLoggingResult result) where T : ICommand;
    }
}
