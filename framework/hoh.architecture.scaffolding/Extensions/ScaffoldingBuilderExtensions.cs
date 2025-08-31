using hoh.architecture.CQRS.Logging;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace hoh.architecture.scaffolding.Extensions
{
    public static class ScaffoldingBuilderExtensions
    {
        public static IApplicationBuilder UseHohArchitecture(this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope();
            var dbContext = serviceScope.ServiceProvider.GetService<LoggingDbContext>();
            var dbCreated = dbContext?.Database.EnsureCreated();

            if (dbCreated.HasValue && !dbCreated.Value)
            {
                dbContext?.Database.Migrate();
            }

            //TODO any app setup should go here
            return app;
        }
    }
}
