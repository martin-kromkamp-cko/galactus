using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Processing.Configuration.MerchantCategoryCodes;

namespace Processing.Configuration.Infra.Data.Processing.MerchantCategoryCodes;

public class MerchantCategoryCodeConfig : IEntityTypeConfiguration<MerchantCategoryCode>
{
    public void Configure(EntityTypeBuilder<MerchantCategoryCode> builder)
    {
        builder.ToTable("merchant_category_code");
        builder.HasQueryFilter(x => x.IsActive);
        
        builder.HasKey(x => x.Id);

        builder.HasIndex(x => x.ExternalId)
            .IsUnique();

        builder.HasIndex(x => x.Code)
            .IsUnique();

        builder.Property(x => x.Title)
            .IsRequired();

        builder.Property(x => x.IsActive)
            .IsRequired();
        
        builder.Property(x => x.CreatedOn)
            .HasDefaultValueSql("now()");
        
        builder.Property(x => x.UpdatedOn)
            .IsRequired()
            .HasDefaultValueSql("now()");
    }
}