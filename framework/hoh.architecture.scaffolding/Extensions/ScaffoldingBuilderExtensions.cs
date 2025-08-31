using HoH.Architecture.CQRS.Logging;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace HoH.Architecture.scaffolding.Extensions
{
    public static class ScaffoldingBuilderExtensions
    {
        public static IApplicationBuilder UseHohArchitecture(this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope();
            var dbContext = serviceScope.ServiceProvider.GetService<LoggingDbContext>();
            var dbCreated = dbContext?.Database.EnsureCreated();

            dbContext.Database.GetPendingMigrations();
            if (dbCreated.HasValue && !dbCreated.Value)
            {
                // Check and apply pending migrations
                var pendingMigrations = dbContext.Database.GetPendingMigrations();
                if (pendingMigrations.Any())
                {
                    Console.WriteLine("Applying pending migrations...");
                    dbContext.Database.Migrate();
                    Console.WriteLine("Migrations applied successfully.");
                }
                else
                {
                    Console.WriteLine("No pending migrations found.");
                }
            }

            //TODO any app setup should go here
            return app;
        }
    }
}
