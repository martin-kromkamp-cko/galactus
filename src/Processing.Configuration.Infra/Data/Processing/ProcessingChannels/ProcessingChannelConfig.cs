using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Processing.Configuration.ProcessingChannels;

namespace Processing.Configuration.Infra.Data.Processing.ProcessingChannels;

public class ProcessingChannelConfig : IEntityTypeConfiguration<ProcessingChannel>
{
    public void Configure(EntityTypeBuilder<ProcessingChannel> builder)
    {
        builder.ToTable("processing_channel");
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

        builder.Property(x => x.Name)
            .IsRequired();
        
        builder.Property(x => x.BusinessModel)
            .IsRequired();
        
        builder.HasMany(x => x.Processors)
            .WithOne(x => x.ProcessingChannel);

        builder.HasMany(x => x.Services)
            .WithOne();
    }
}