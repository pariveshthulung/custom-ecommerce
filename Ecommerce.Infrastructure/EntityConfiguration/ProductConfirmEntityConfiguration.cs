namespace Ecommerce.Infrastructure.EntityConfiguration;

public class ProductConfirmEntityConfiguration : IEntityTypeConfiguration<ProductConfirm>
{
    public void Configure(EntityTypeBuilder<ProductConfirm> builder)
    {
        builder.Property(x => x.ProductItemId).IsRequired();
        builder.Property(x => x.InvarientOptionId).IsRequired();
    }
}
