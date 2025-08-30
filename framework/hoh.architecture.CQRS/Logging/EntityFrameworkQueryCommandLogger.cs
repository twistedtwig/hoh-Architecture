using hoh.architecture.CQRS.Command;
using hoh.architecture.CQRS.Query;
using hoh.architecture.Shared.Configuration;
using Microsoft.Extensions.Options;

namespace hoh.architecture.CQRS.Logging
{
    public class EntityFrameworkQueryCommandLogger : IQueryCommandLogging
    {
        private readonly IOptions<HohArchitectureOptions> _hohOptions;
        public EntityFrameworkQueryCommandLogger(IOptions<HohArchitectureOptions> options)
        {
            _hohOptions = options;
        }
        // public Task RegisterServiceAsync(IApplicationBuilder app, IServiceCollection serviceCollection)
        // {
        //     serviceCollection.AddDbContext<LoggingDbContext>(options =>
        //     {
        //         //TODO split into two for command and query dbs
        //         options
        //             .UseSqlServer(_hohOptions.Value.CommandLogging.CommandLoggingConnectionString)
        //             .EnableSensitiveDataLogging(_hohOptions.Value.CommandLogging.EnableSensitiveDataLogging);
        //     });
        //
        //     throw new NotImplementedException();
        // }

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
