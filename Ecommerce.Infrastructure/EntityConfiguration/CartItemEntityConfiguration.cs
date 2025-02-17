namespace Ecommerce.Infrastructure.EntityConfiguration;

public class CartItemEntityConfiguration : IEntityTypeConfiguration<CartItem>
{
    public void Configure(EntityTypeBuilder<CartItem> builder)
    {
        builder.ToTable(
            nameof(CartItem).Pluralize().Pascalize(),
            EcommerceDbContext.ECOMMERCE_SCHEMA
        );
        builder.Property(e => e.CartId).IsRequired();
        builder.Property(e => e.ProductId).IsRequired();
        builder.Property(e => e.AddedDate).IsRequired();
        builder.Property(e => e.IsChecked).HasDefaultValue(true);
    }
}
