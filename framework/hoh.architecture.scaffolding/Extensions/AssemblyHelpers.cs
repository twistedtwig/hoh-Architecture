using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using HoH.Architecture.CQRS.Command;
using HoH.Architecture.CQRS.Query;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace HoH.Architecture.scaffolding.Extensions
{
    internal static class AssemblyHelpers
    {
        internal static void RegisterQueryHandlers(IServiceCollection serviceCollection, ServiceLifetime lifetime, params Assembly[] assemblies)
        {
            Register(serviceCollection, typeof(IQueryHandler<,>), lifetime, assemblies);
        }

        internal static void RegisterCommandHandlers(IServiceCollection serviceCollection, ServiceLifetime lifetime, params Assembly[] assemblies)
        {
            Register(serviceCollection, typeof(ICommandHandler<>), lifetime, assemblies);
        }

        private static void Register(IServiceCollection serviceCollection, Type baseInterface, ServiceLifetime lifetime = ServiceLifetime.Scoped, params Assembly[] assemblies)
        {
            var types = assemblies.SelectMany(a => a.GetTypes());

            var data = types
                .Where(t => t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == baseInterface))
                .Select(t => new { assignedType = t, serviceTypes = t.GetInterfaces().ToList() })
                .ToList();

            foreach (var item in data)
            {
                foreach (var i in item.serviceTypes)
                {
                    switch (lifetime)
                    {
                        case ServiceLifetime.Scoped:
                            serviceCollection.TryAddScoped(i, item.assignedType);
                            break;

                        case ServiceLifetime.Singleton:
                        case ServiceLifetime.Transient:
                        default:
                            throw new ArgumentOutOfRangeException(nameof(lifetime), lifetime, null);
                    }
                }
            }
        }
    }
}
