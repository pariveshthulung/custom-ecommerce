namespace Ecommerce.Infrastructure.EntityConfiguration;

public class ProductConfirmEntityConfiguration : IEntityTypeConfiguration<ProductConfirm>
{
    public void Configure(EntityTypeBuilder<ProductConfirm> builder)
    {
        builder.ToTable(
            nameof(ProductConfirm).Pluralize().Pascalize(),
            EcommerceDbContext.ECOMMERCE_SCHEMA
        );
        builder.Property(x => x.ProductItemId).IsRequired();
        builder.Property(x => x.InvarientOptionId).IsRequired();
    }
}
