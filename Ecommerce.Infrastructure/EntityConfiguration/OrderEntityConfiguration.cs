namespace Ecommerce.Infrastructure.EntityConfiguration;

public class OrderEntityConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.Property(e => e.CustomerId).IsRequired();
        builder.Property(e => e.OrderedDate).IsRequired();
        builder
            .HasMany(e => e.OrderItems)
            .WithOne()
            .HasForeignKey(e => e.OrderId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
