using hoh.architecture.Shared.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace hoh.architecture.CQRS.Logging
{
    public class LoggingDbContext : DbContext
    {
        private HohArchitectureOptions hohOptions;

        public LoggingDbContext(DbContextOptions<LoggingDbContext> options, IOptions<HohArchitectureOptions> hohOptions) : base(options)
        {
            this.hohOptions = hohOptions.Value;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<LoggingEntity>()
                .ToTable(hohOptions.TableName)
                .HasKey(x => x.Id);
        }
    }
}
