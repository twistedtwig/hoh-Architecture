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
            Console.WriteLine($"AddHohArchitecture start");
            var options = HohArchitectureOptions.Default;
            configureOptions?.Invoke(options);
            Console.WriteLine($"0 config setup, use service {options.UseServiceCollection}, {options.CommandLogging.CommandLoggingConnectionString} {options.CommandLogging.TableName}");

            services.AddOptions<HohArchitectureOptions>().Configure(hohOptions =>
            {
                Console.WriteLine($"1 config setup, use service {hohOptions.UseServiceCollection}, {hohOptions.CommandLogging.CommandLoggingConnectionString} {hohOptions.CommandLogging.TableName}");

                if (options.UseServiceCollection.HasValue)
                {
                    hohOptions.UseServiceCollection = options.UseServiceCollection;
                }

                if (options.CommandLogging.Type.HasValue)
                {
                    hohOptions.CommandLogging.Type = options.CommandLogging.Type;
                    Console.WriteLine($"setting CommandLogging.Type  {hohOptions.CommandLogging.Type}");
                }

                if (!string.IsNullOrWhiteSpace(options.CommandLogging.CommandLoggingConnectionString))
                {
                    hohOptions.CommandLogging.CommandLoggingConnectionString= options.CommandLogging.CommandLoggingConnectionString;
                    Console.WriteLine($"setting CommandLogging.CommandLoggingConnectionString  {hohOptions.CommandLogging.CommandLoggingConnectionString}");
                }

                if (!string.IsNullOrWhiteSpace(options.CommandLogging.TableName))
                {
                    hohOptions.CommandLogging.TableName = options.CommandLogging.TableName;
                    Console.WriteLine($"setting CommandLogging.TableName  {hohOptions.CommandLogging.TableName}");
                }

                if (options.QueryLogging.Type.HasValue)
                {
                    hohOptions.QueryLogging.Type = options.QueryLogging.Type;
                    Console.WriteLine($"setting QueryLogging.Type  {hohOptions.QueryLogging.Type}");
                }

                if (!string.IsNullOrWhiteSpace(options.QueryLogging.QueryLoggingConnectionString))
                {
                    hohOptions.QueryLogging.QueryLoggingConnectionString = options.QueryLogging.QueryLoggingConnectionString;
                    Console.WriteLine($"setting QueryLogging.QueryLoggingConnectionString  {hohOptions.QueryLogging.QueryLoggingConnectionString}");
                }

                if (!string.IsNullOrWhiteSpace(options.QueryLogging.TableName))
                {
                    hohOptions.QueryLogging.TableName = options.QueryLogging.TableName;
                    Console.WriteLine($"setting QueryLogging.TableName  {hohOptions.QueryLogging.TableName}");
                }

                Console.WriteLine($"2 config setup, use service {hohOptions.UseServiceCollection}, {hohOptions.CommandLogging.CommandLoggingConnectionString} {hohOptions.CommandLogging.TableName}");
            });

            if (options.UseServiceCollection.HasValue && options.UseServiceCollection.Value)
            {
                Console.WriteLine("HandleRegisterServices");
                services.AddScoped<IQueryCommandExecutor, QueryCommandExecutor>();
                services.AddScoped<IQueryCommandLocator, ServiceProviderQueryCommandLocator>();
            }

            //TODO will register services such as CQRS factories


            //TODO register IRepository

            services.Configure(configureOptions);

            Console.WriteLine($"AddHohArchitecture after options");
            //once all config has been applied, ensure services are configured correctly
            services.PostConfigure<HohArchitectureOptions>(options =>
            {
                Console.WriteLine("in post config setup area");
                //HandleRegisterServices(services, options);
                HandleQueryLogging(options);
                HandleCommandLogging(options);
            });

            Console.WriteLine($"AddHohArchitecture end");
            return services;
        }

        //private static void HandleRegisterServices(IServiceCollection services, HohArchitectureOptions options)
        //{
        //    if (options.UseServiceCollection)
        //    {
        //        Console.WriteLine("HandleRegisterServices");
        //        services.AddScoped<IQueryCommandExecutor, QueryCommandExecutor>();
        //        services.AddScoped<IQueryCommandLocator, ServiceProviderQueryCommandLocator>();
        //    }
        //}

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
