using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace HoH.Architecture.scaffolding.Extensions
{
    public static class EntityFrameWorkExtensions
    {
        public static void CreateDatabaseRunMigrations<TDB>(this IApplicationBuilder app) where TDB : DbContext
        {
            using var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope();
            var dbContext = serviceScope.ServiceProvider.GetService<TDB>();
            var dbCreated = dbContext?.Database.EnsureCreated();

            dbContext.Database.GetPendingMigrations();
            if (dbCreated.HasValue && !dbCreated.Value)
            {
                // Check and apply pending migrations
                var pendingMigrations = dbContext.Database.GetPendingMigrations();
                if (pendingMigrations.Any())
                {
                    dbContext.Database.Migrate();
                }
            }
        }
    }
}
