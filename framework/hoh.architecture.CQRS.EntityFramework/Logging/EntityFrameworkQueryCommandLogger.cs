using hoh.architecture.CQRS.Command;
using hoh.architecture.CQRS.Logging;
using hoh.architecture.CQRS.Query;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace hoh.architecture.CQRS.EntityFramework.Logging
{
    public class EntityFrameworkQueryCommandLogger : IQueryCommandLogging
    {
        public Task RegisterServiceAsync(IApplicationBuilder app, IServiceCollection serviceCollection)
        {
            //get hohoptions
            var configuration = app.ApplicationServices.GetService<IConfiguration>();
            HohArchitectureOptions options;

            serviceCollection.AddDbContext<LoggingDbContext>(options =>
            {
                options.use
            });

            throw new NotImplementedException();
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
