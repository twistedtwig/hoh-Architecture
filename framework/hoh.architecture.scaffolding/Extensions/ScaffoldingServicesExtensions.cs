using Microsoft.Extensions.DependencyInjection;
using hoh.architecture.CQRS.Query;
using hoh.architecture.scaffolding.Configuration;

namespace hoh.architecture.scaffolding.Extensions
{
    public static class ScaffoldingServicesExtensions
    {
        public static IServiceCollection AddHohArchitecture(this IServiceCollection services)
        {
            return AddHohArchitecture(services, null);
        }

        public static IServiceCollection AddHohArchitecture(this IServiceCollection services, Action<HohArchitectureOptions>? configureOptions)
        {
            var defaultOptions = HohArchitectureOptions.Default;
            services.AddOptions<HohArchitectureOptions>().Configure(options =>
            {
                options.CommandLogging = defaultOptions.CommandLogging;
                options.QueryLogging = defaultOptions.QueryLogging;
            });

            services.AddTransient<IQueryExecutor, QueryExecutor>();

            //TODO will register services such as CQRS factories
            //TODO register IRepository

            if (configureOptions != null)
            {
                //apply configuration options provided by the consumer
                services.Configure(configureOptions);

                //once all config has been applied, ensure services are configured correctly
                services.PostConfigure<HohArchitectureOptions>(x =>
                {
                    HandleQueryLogging(x);

                    HandleCommandLogging(x);
                });
            }
            

            return services;
        }

        private static void HandleQueryLogging(HohArchitectureOptions x)
        {            
            switch (x.QueryLogging.Type)
            {
                case CommandQueryLoggingType.None:
                    //TODO register blank query and command loggers
                    break;
                case CommandQueryLoggingType.BuiltInEfProvider:
                //TODO register ef logger
                    break;
                case CommandQueryLoggingType.Custom:
                    break;
            }
        }

        private static void HandleCommandLogging(HohArchitectureOptions x)
        {
            switch (x.CommandLogging.Type)
            {
                case CommandQueryLoggingType.None:
                    //TODO register blank query and command loggers
                    break;
                case CommandQueryLoggingType.BuiltInEfProvider:
                    //TODO register ef logger
                    break;
                case CommandQueryLoggingType.Custom:
                    break;
            }
        }
    }
}
