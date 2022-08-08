using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Processing.Configuration.Processors;

namespace Processing.Configuration.Infra.Data.Processing.Processors;

public class CkoServiceConfig : IEntityTypeConfiguration<CkoService>
{
    public void Configure(EntityTypeBuilder<CkoService> builder)
    {
        builder.ToTable("cko_service");
        builder.HasQueryFilter(x => x.IsActive);
        
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
    }
}