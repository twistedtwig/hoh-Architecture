using Microsoft.Extensions.DependencyInjection;
using hoh.architecture.CQRS.Query;

namespace hoh.architecture.scaffolding.Extensions
{
    public static class ScaffoldingServicesExtensions
    {
        public static IServiceCollection AddHohArchitecture(this IServiceCollection services)
        {
            services.AddTransient<IQueryExecutor, QueryExecutor>();

            //TODO will register services such as CQRS factories
            return services;
        }
    }
}
