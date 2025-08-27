using hoh.architecture.CQRS.Logging;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace hoh.architecture.scaffolding.Extensions
{
    public static class ScaffoldingBuilderExtensions
    {
        public static IApplicationBuilder UseHohArchitecture(this IApplicationBuilder app, IServiceCollection serviceCollection)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var loggingService = scope.ServiceProvider.GetService<IQueryCommandLogging>();
                if (loggingService != null)
                {
                    loggingService.RegisterServiceAsync(app, serviceCollection);
                }
            }

            return app;
        }
    }
}
