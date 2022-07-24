using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Processing.Configuration.Currencies;

namespace Processing.Configuration.Infra.Data.Currencies;

public class CurrencyConfig : IEntityTypeConfiguration<Currency>
{
    public void Configure(EntityTypeBuilder<Currency> builder)
    {
        builder.ToTable("currency");
        
        builder.HasKey(x => x.Id);
        builder.HasIndex(x => x.Code)
            .IsUnique();
        
        builder.Property(x => x.Country)
            .IsRequired();
        
        builder.Property(x => x.Name)
            .IsRequired();
        
        builder.Property(x => x.Number)
            .IsRequired();

        builder.Property(x => x.IsActive)
            .IsRequired();
        
        builder.Property(x => x.CreatedOn)
            .HasDefaultValueSql("now()");
        
        builder.Property(x => x.CreatedOn)
            .IsRequired()
            .HasDefaultValueSql("now()");
    }
}