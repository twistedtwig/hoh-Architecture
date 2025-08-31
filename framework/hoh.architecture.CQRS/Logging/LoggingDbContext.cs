using Microsoft.EntityFrameworkCore;

namespace HoH.Architecture.CQRS.Logging
{
    public class LoggingDbContext : DbContext
    {
        public virtual string TableName => "CommandQueryExecutionLogs";
        public LoggingDbContext(DbContextOptions<LoggingDbContext> options) : base(options)
        {
            Console.WriteLine("loggind db context constructor");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Console.WriteLine($"loggind db context on model creating, {TableName}");
            modelBuilder.Entity<LoggingEntity>()
                .ToTable(TableName)
                .HasKey(x => x.Id);
        }
    }
}
