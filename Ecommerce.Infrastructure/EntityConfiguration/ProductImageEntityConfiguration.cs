namespace Ecommerce.Infrastructure.EntityConfiguration;

public class ProductImageEntityConfiguration : IEntityTypeConfiguration<ProductImage>
{
    public void Configure(EntityTypeBuilder<ProductImage> builder)
    {
        builder.Property(e => e.ProductId).IsRequired();
        builder.Property(e => e.Image).HasMaxLength(255).IsRequired();
    }
}
