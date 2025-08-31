using hoh.architecture.Shared.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Options;

namespace hoh.architecture.scaffolding.Extensions
{
    public static class ScaffoldingBuilderExtensions
    {
        public static IApplicationBuilder UseHohArchitecture(this IApplicationBuilder app)
        {
            //TODO any app setup should go here
            return app;
        }
    }
}
