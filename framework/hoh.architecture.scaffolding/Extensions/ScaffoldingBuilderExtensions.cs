using HoH.Architecture.CQRS.Logging;
using Microsoft.AspNetCore.Builder;

namespace HoH.Architecture.scaffolding.Extensions
{
    public static class ScaffoldingBuilderExtensions
    {
        public static IApplicationBuilder UseHohArchitecture(this IApplicationBuilder app)
        {
            app.CreateDatabaseRunMigrations<LoggingDbContext>();

            //TODO any app setup should go here
            return app;
        }
    }
}
