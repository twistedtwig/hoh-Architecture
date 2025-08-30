using Microsoft.AspNetCore.Builder;

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
