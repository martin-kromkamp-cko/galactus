using Microsoft.EntityFrameworkCore;

namespace Processing.Configuration.Infra.Data.Auditing;

public class AuditContext : DbContext
{
    public AuditContext(DbContextOptions<AuditContext> options) : base(options)
    {
    }

    public DbSet<AuditEvent> AuditEvents { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AuditEvent>().ToTable("audit_event");
        modelBuilder.Entity<AuditEvent>().HasKey(x => x.Id);
        modelBuilder.Entity<AuditEvent>().Property(x => x.Data).HasColumnType("jsonb");
        modelBuilder.Entity<AuditEvent>().Property(x => x.InsertedDate).HasDefaultValueSql("now()");
        modelBuilder.Entity<AuditEvent>().Property(x => x.UpdatedDate).HasDefaultValueSql("now()");
    }
}