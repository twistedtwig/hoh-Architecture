using hoh.architecture.Shared.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
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
            modelBuilder.ApplyConfiguration(new LoggingEntityConfiguration(hohOptions));
        }
    }

    public class LoggingEntityConfiguration : IEntityTypeConfiguration<LoggingEntity>
    {
        private HohArchitectureOptions hohOptions { get; }
        
        public LoggingEntityConfiguration(HohArchitectureOptions hohOptions)
        {
            this.hohOptions = hohOptions;
        }
        public void Configure(EntityTypeBuilder<LoggingEntity> builder)
        {
            builder.ToTable(hohOptions.TableName);
            builder.HasKey(x => x.Id);
        }
    }
}
