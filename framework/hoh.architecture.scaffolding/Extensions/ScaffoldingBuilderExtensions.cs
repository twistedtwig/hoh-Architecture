using Microsoft.AspNetCore.Builder;

namespace hoh.architecture.scaffolding.Extensions
{
    public static class ScaffoldingBuilderExtensions
    {
        public static IApplicationBuilder UseHohArchitecture(this IApplicationBuilder app)
        {
            //TODO will setup options and configuration here
            return app;
        }
    }
}
