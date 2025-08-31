using Microsoft.EntityFrameworkCore;

namespace HoH.Architecture.CQRS.Logging
{
    public class LoggingDbContext : DbContext
    {
        public virtual string TableName => "CommandQueryExecutionLogs";
        public LoggingDbContext(DbContextOptions<LoggingDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LoggingEntity>()
                .ToTable(TableName)
                .HasKey(x => x.Id);
        }
    }
}
