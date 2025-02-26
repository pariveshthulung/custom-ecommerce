namespace Ecommerce.Infrastructure.EntityConfiguration;

public class ProductItemEntityConfiguration : IEntityTypeConfiguration<ProductItem>
{
    public void Configure(EntityTypeBuilder<ProductItem> builder)
    {
        builder.ToTable(
            nameof(ProductItem).Pluralize().Pascalize(),
            EcommerceDbContext.ECOMMERCE_SCHEMA
        );
        builder.Property(x => x.ProductId).IsRequired();
        builder.Property(x => x.Image).HasMaxLength(255).IsRequired();
        builder.Property(x => x.SKU).HasMaxLength(100).IsRequired();
        builder.Property(x => x.Quantity).IsRequired();
        builder.Property(x => x.Price).IsRequired();
    }
}
