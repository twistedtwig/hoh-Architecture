using hoh.architecture.CQRS.Shared.QueryCommandHandling;
using hoh.architecture.scaffolding.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

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
            var options = HohArchitectureOptions.Default;
            configureOptions?.Invoke(options);

            /*
             * apply configuration from bindings.
             * options above will have defaults
             * the action values from initial setup will be applied on top
             *
             * the config method can have null values in hohOptions
             */
            services.AddOptions<HohArchitectureOptions>().Configure(hohOptions =>
            {
                if (hohOptions == null)
                {
                    hohOptions = options;
                    return;
                }

                hohOptions.UseServiceCollection ??= options.UseServiceCollection;

                if (hohOptions.CommandLogging == null)
                {
                    hohOptions.CommandLogging = options.CommandLogging;
                }
                else
                {
                    hohOptions.CommandLogging.Type ??= options.CommandLogging.Type;

                    if (string.IsNullOrWhiteSpace(hohOptions.CommandLogging.TableName))
                    {
                        hohOptions.CommandLogging.TableName = options.CommandLogging.TableName;
                    }

                    if (string.IsNullOrWhiteSpace(hohOptions.CommandLogging.CommandLoggingConnectionString))
                    {
                        hohOptions.CommandLogging.CommandLoggingConnectionString = options.CommandLogging.CommandLoggingConnectionString;
                    }
                }

                if (hohOptions.QueryLogging == null)
                {
                    hohOptions.QueryLogging = options.QueryLogging;
                }
                else
                {
                    hohOptions.QueryLogging.Type ??= options.QueryLogging.Type;

                    if (string.IsNullOrWhiteSpace(hohOptions.QueryLogging.TableName))
                    {
                        hohOptions.QueryLogging.TableName = options.QueryLogging.TableName;
                    }

                    if (string.IsNullOrWhiteSpace(hohOptions.QueryLogging.QueryLoggingConnectionString))
                    {
                        hohOptions.QueryLogging.QueryLoggingConnectionString = options.QueryLogging.QueryLoggingConnectionString;
                    }
                }
            });

            if (options.UseServiceCollection.HasValue && options.UseServiceCollection.Value)
            {
                services.AddScoped<IQueryCommandExecutor, QueryCommandExecutor>();
                services.AddScoped<IQueryCommandLocator, ServiceProviderQueryCommandLocator>();
            }



            //TODO will register services such as CQRS factories


            //TODO register IRepository

            //once all config has been applied, ensure services are configured correctly
            services.PostConfigure<HohArchitectureOptions>(options =>
            {
                HandleQueryLogging(options);
                HandleCommandLogging(options);
            });

            return services;
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
