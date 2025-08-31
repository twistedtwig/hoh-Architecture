using hoh.architecture.CQRS.Shared.QueryCommandHandling;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using hoh.architecture.CQRS.Logging;
using hoh.architecture.Shared.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace hoh.architecture.scaffolding.Extensions
{
    public static class ScaffoldingServicesExtensions
    {
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

        public static IServiceCollection AddHohArchitecture(this IServiceCollection services)
        {
            return AddHohArchitecture(services, null);
        }

        public static IServiceCollection AddHohArchitecture(this IServiceCollection services, Action<HohArchitectureOptions>? configureOptions)
        {
            SetupHohArchitectureOptions(services, configureOptions);

            return services;
        }

        public static IServiceCollection AddHohArchitecture<TL, TDb>(
            this IServiceCollection services,
            Action<HohArchitectureOptions>? configureOptions,
            ServiceLifetime lifetime = ServiceLifetime.Scoped)
            where TDb : DbContext
            where TL : class, ICommandQueryLogging
        {
            SetupHohArchitectureOptions(services, configureOptions);
            RegisterCommandQueryLogging<TL>(services, lifetime);

            services.AddDbContext<TDb>((sp, builder) =>
            {
                var hohOptions = sp.GetRequiredService<IOptions<HohArchitectureOptions>>();
                builder.UseSqlServer(hohOptions.Value.ConnectionString);
                builder.EnableSensitiveDataLogging(hohOptions.Value.EnableSensitiveDataLogging);
            });

            return services;
        }

        private static void SetupHohArchitectureOptions(IServiceCollection services, Action<HohArchitectureOptions>? configureOptions)
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

                if (string.IsNullOrWhiteSpace(hohOptions.TableName))
                {
                    hohOptions.TableName = options.TableName;
                }

                if (string.IsNullOrWhiteSpace(hohOptions.ConnectionString))
                {
                    hohOptions.ConnectionString = options.ConnectionString;
                }
            });

            if (options.UseServiceCollection.HasValue && options.UseServiceCollection.Value)
            {
                services.AddScoped<IQueryCommandExecutor, QueryCommandExecutor>();
                services.AddScoped<IQueryCommandLocator, ServiceProviderQueryCommandLocator>();
            }

            services.PostConfigure<HohArchitectureOptions>(options => { });
        }

        private static void RegisterCommandQueryLogging<T>(IServiceCollection services, ServiceLifetime lifetime = ServiceLifetime.Scoped)
            where T : class, ICommandQueryLogging
        {
            switch (lifetime)
            {
                case ServiceLifetime.Singleton:
                    services.AddSingleton<ICommandQueryLogging, T>();
                    break;

                case ServiceLifetime.Scoped:
                    services.AddScoped<ICommandQueryLogging, T>();
                    break;

                case ServiceLifetime.Transient:
                    services.AddTransient<ICommandQueryLogging, T>();
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(lifetime), lifetime, null);
            }
        }
    }
}
