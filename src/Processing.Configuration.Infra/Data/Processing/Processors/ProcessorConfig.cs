using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Processing.Configuration.Currencies;
using Processing.Configuration.MerchantCategoryCodes;
using Processing.Configuration.Processors;

namespace Processing.Configuration.Infra.Data.Processing.Processors;

public class ProcessorConfig : IEntityTypeConfiguration<Processor>
{
    public void Configure(EntityTypeBuilder<Processor> builder)
    {
        builder.ToTable("processor");
        
        builder.HasKey(x => x.Id);

        builder.HasIndex(x => x.ExternalId)
            .IsUnique();

        builder.Property(x => x.IsActive)
            .IsRequired();
        
        builder.Property(x => x.CreatedOn)
            .HasDefaultValueSql("now()");
        
        builder.Property(x => x.UpdatedOn)
            .IsRequired()
            .HasDefaultValueSql("now()");

        builder.HasMany(x => x.Currencies)
            .WithMany(x => x.Processors);

        builder.HasMany(x => x.Schemes)
            .WithMany(x => x.Processors);

        builder.HasOne(x => x.MerchantCategoryCode)
            .WithMany(x => x.Processors);

        builder.HasMany(x => x.Services)
            .WithOne();

        builder.HasOne(x => x.ProcessingChannel)
            .WithMany(x => x.Processors);
    }
}