using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Processing.Configuration.Currencies;
using Processing.Configuration.MerchantCategoryCodes;
using Processing.Configuration.Processors;

namespace Processing.Configuration.Infra.Data.Processing.Processors;

public class ProcessorAcceptorConfig : IEntityTypeConfiguration<ProcessorAcceptor>
{
    public void Configure(EntityTypeBuilder<ProcessorAcceptor> builder)
    {
        builder.ToTable("processor_acceptor");
        
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

        builder.HasOne(x => x.Processor)
            .WithOne(x => x.Acceptor);
    }
}