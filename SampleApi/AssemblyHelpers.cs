//using System.Reflection;
//using hoh.architecture.CQRS.Query;
//using Microsoft.Extensions.DependencyInjection.Extensions;

//namespace SampleApi
//{
//    internal static class AssemblyHelpers
//    {
//        internal static void RegisterQueryHandlers(this IServiceCollection serviceCollection, ServiceLifetime lifetime, params Assembly[] assemblies)
//        {
//            Register(serviceCollection, typeof(IQueryHandler<,>), lifetime, assemblies);
//        }

//        private static void Register(this IServiceCollection serviceCollection, Type baseInterface, ServiceLifetime lifetime = ServiceLifetime.Scoped, params Assembly[] assemblies)
//        {
//            var types = assemblies.SelectMany(a => a.GetTypes());

//            var data = types
//                .Where(t => t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == baseInterface))
//                .Select(t => new { assignedType = t, serviceTypes = t.GetInterfaces().ToList() })
//                .ToList();

//            foreach (var item in data)
//            {
//                foreach (var i in item.serviceTypes)
//                {
//                    switch (lifetime)
//                    {
//                        case ServiceLifetime.Scoped:
//                            serviceCollection.TryAddScoped(i, item.assignedType);
//                            break;

//                        case ServiceLifetime.Singleton:
//                        case ServiceLifetime.Transient:
//                        default:
//                            throw new ArgumentOutOfRangeException(nameof(lifetime), lifetime, null);
//                    }
//                }
//            }
//        }
//    }
//}
