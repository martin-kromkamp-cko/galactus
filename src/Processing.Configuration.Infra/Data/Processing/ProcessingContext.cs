using System.Reflection;
using EntityFrameworkCore.ChangeEvents;
using Microsoft.EntityFrameworkCore;
using Processing.Configuration.Currencies;
using Processing.Configuration.MerchantCategoryCodes;
using Processing.Configuration.Schemes;

namespace Processing.Configuration.Infra.Data.Processing;

public class ProcessingContext : DbContext
{
    public ProcessingContext(DbContextOptions<ProcessingContext> options) 
        : base(options)
    {
    }

    public DbSet<Currency> Currencies { get; set; }
    
    public DbSet<CardScheme> CardSchemes { get; set; }
    
    public DbSet<MerchantCategoryCode> MerchantCategoryCodes { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetAssembly(typeof(ProcessingContext)));
        
        // Add change event model.
        modelBuilder.AddChangeEvents<ChangeEvent>();
    }
}