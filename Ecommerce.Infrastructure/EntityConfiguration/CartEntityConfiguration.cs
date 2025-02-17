namespace Ecommerce.Infrastructure.EntityConfiguration;

public class CartEntityConfiguration : IEntityTypeConfiguration<Cart>
{
    public void Configure(EntityTypeBuilder<Cart> builder)
    {
        builder.ToTable(
            nameof(Cart).Humanize().Pluralize().Pascalize(),
            EcommerceDbContext.ECOMMERCE_SCHEMA
        );
        builder
            .HasMany(e => e.CartItems)
            .WithOne()
            .HasForeignKey(e => e.CartId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.Property(e => e.UserId).IsRequired();
    }
}
