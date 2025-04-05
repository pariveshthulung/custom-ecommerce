namespace Ecommerce.Infrastructure.EntityConfiguration;

public class StoreEntityConfiguration : IEntityTypeConfiguration<Store>
{
    public void Configure(EntityTypeBuilder<Store> builder)
    {
        builder.ToTable(nameof(Store).Pluralize().Pascalize(), EcommerceDbContext.ECOMMERCE_SCHEMA);
        builder.Property(x => x.MaxUser).IsRequired();
        builder.Property(x => x.StoreName).IsRequired();
        // builder
        //     .Property(c => c.ProductsId)
        //     .HasConversion(
        //         v => string.Join(",", v), // Convert List<long> to a comma-separated string for storage
        //         v => v.Split(",", StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToList() // Convert string back to List<long>
        //     );
        builder
            .Property(c => c.AdministratorsId)
            .HasConversion(
                v => string.Join(",", v), // Convert List<long> to a comma-separated string for storage
                v => v.Split(",", StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToList() // Convert string back to List<long>
            );
        builder.Property(x => x.MaxUser).IsRequired();
    }
}
