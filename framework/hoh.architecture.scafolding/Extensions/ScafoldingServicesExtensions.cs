using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hoh.architecture.scafolding.Extensions
{
    public static class ScafoldingServicesExtensions
    {
        public static IServiceCollection AddHohArchitecture(this IServiceCollection services)
        {
            //TODO will register services such as CQRS factories
            return services;
        }
    }
}
