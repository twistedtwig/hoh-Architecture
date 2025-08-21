using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using hoh.architecture.scaffolding.Configuration;
using hoh.architecture.CQRS.Shared.QueryCommandHandling;

namespace hoh.architecture.scaffolding.Extensions
{
    public static class ScaffoldingServicesExtensions
    {
        public static IServiceCollection AddHohArchitecture(this IServiceCollection services)
        {
            return AddHohArchitecture(services, null);
        }

        public static IServiceCollection RegisterQueryHandlers(this IServiceCollection services, ServiceLifetime lifetime, params Assembly[] assemblies)
        {
            AssemblyHelpers.RegisterQueryHandlers(services, lifetime, assemblies);

            return services;
        }

        public static IServiceCollection RegisterCommandHandlers(this IServiceCollection services, ServiceLifetime lifetime, params Assembly[] assemblies)
        {
            AssemblyHelpers.RegisterCommandHandlers(services, lifetime, assemblies);

            return services;
        }

        public static IServiceCollection AddHohArchitecture(this IServiceCollection services, Action<HohArchitectureOptions>? configureOptions)
        {
            Console.WriteLine($"AddHohArchitecture: {configureOptions = null}");
            var defaultOptions = HohArchitectureOptions.Default;
            configureOptions?.Invoke(defaultOptions);

            services.AddOptions<HohArchitectureOptions>().Configure(options =>
            {
                Console.WriteLine($"1 config setup, use service {options.UseServiceCollection}, {options.CommandLogging.CommandLoggingConnectionString}");
                options.UseServiceCollection = defaultOptions.UseServiceCollection;
                options.CommandLogging = defaultOptions.CommandLogging;
                options.QueryLogging = defaultOptions.QueryLogging;

                Console.WriteLine($"2 config setup, use service {options.UseServiceCollection}, {options.CommandLogging.CommandLoggingConnectionString}");
            });

            //TODO will register services such as CQRS factories


            //TODO register IRepository

            // services.Configure(configureOptions);

            //once all config has been applied, ensure services are configured correctly
            services.PostConfigure<HohArchitectureOptions>(options =>
            {
                Console.WriteLine("in post config setup area");
                HandleRegisterServices(services, options);
                HandleQueryLogging(options);
                HandleCommandLogging(options);
            });

            return services;
        }

        private static void HandleRegisterServices(IServiceCollection services, HohArchitectureOptions options)
        {
            if (options.UseServiceCollection)
            {
                Console.WriteLine("HandleRegisterServices");
                services.AddScoped<IQueryCommandExecutor, QueryCommandExecutor>();
                services.AddScoped<IQueryCommandLocator, ServiceProviderQueryCommandLocator>();
            }
        }

        private static void HandleQueryLogging(HohArchitectureOptions options)
        {
            switch (options.QueryLogging.Type)
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

        private static void HandleCommandLogging(HohArchitectureOptions options)
        {
            switch (options.CommandLogging.Type)
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
