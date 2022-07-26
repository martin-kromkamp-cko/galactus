using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Processing.Configuration.Schemes;

namespace Processing.Configuration.Infra.Data.Processing;

public class CardSchemeConfig : IEntityTypeConfiguration<CardScheme>
{
    public void Configure(EntityTypeBuilder<CardScheme> builder)
    {
        builder.ToTable("card_scheme");
        
        builder.HasKey(x => x.Id);
        
        builder.HasIndex(x => x.ExternalId)
            .IsUnique();
        
        builder.HasIndex(x => x.Scheme)
            .IsUnique();

        builder.Property(x => x.IsActive)
            .IsRequired();
        
        builder.Property(x => x.CreatedOn)
            .HasDefaultValueSql("now()");
        
        builder.Property(x => x.CreatedOn)
            .IsRequired()
            .HasDefaultValueSql("now()");
    }
}