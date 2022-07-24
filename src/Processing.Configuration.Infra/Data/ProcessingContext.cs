using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Processing.Configuration.Currencies;

namespace Processing.Configuration.Infra.Data;

public class ProcessingContext : DbContext
{
    public ProcessingContext(DbContextOptions<ProcessingContext> options) 
        : base(options)
    {
    }

    public DbSet<Currency> Currencies { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetAssembly(typeof(ProcessingContext)));
    }
}