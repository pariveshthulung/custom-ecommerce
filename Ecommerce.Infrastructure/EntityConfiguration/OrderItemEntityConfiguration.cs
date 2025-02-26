namespace Ecommerce.Infrastructure.EntityConfiguration;

public class OrderItemEntityConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.ToTable(
            nameof(OrderItem).Pluralize().Pascalize(),
            EcommerceDbContext.ECOMMERCE_SCHEMA
        );
        builder.Property(e => e.OrderId).IsRequired();
        builder.Property(e => e.Quantity).IsRequired();
        builder.Property(e => e.Price).IsRequired();
    }
}
