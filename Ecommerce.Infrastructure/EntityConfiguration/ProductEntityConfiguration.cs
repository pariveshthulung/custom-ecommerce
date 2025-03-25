namespace Ecommerce.Infrastructure.EntityConfiguration;

public class ProductEntityConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable(
            nameof(Product).Humanize().Pluralize().Pascalize(),
            EcommerceDbContext.ECOMMERCE_SCHEMA
        );
        builder.Property(bt => bt.IsActive).HasDefaultValue(true);
        builder.Property(bt => bt.Name).IsRequired().HasMaxLength(200);
        builder.Property(bt => bt.Description).IsRequired().HasMaxLength(255);
        builder
            .HasMany(bt => bt.ProductImages)
            .WithOne()
            .HasForeignKey(bt => bt.ProductId)
            .OnDelete(DeleteBehavior.Cascade);
        builder
            .HasMany(bt => bt.ProductItems)
            .WithOne()
            .HasForeignKey(bt => bt.ProductId)
            .OnDelete(DeleteBehavior.Cascade);
        builder
            .Property(c => c.CategoriesId)
            .HasConversion(
                v => string.Join(",", v), // Convert List<long> to a comma-separated string for storage
                v => v.Split(",", StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToList() // Convert string back to List<long>
            );
    }
}
