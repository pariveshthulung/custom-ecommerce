namespace Ecommerce.Infrastructure.EntityConfiguration;

public class StoreEntityConfiguration : IEntityTypeConfiguration<Store>
{
    public void Configure(EntityTypeBuilder<Store> builder)
    {
        builder.ToTable(nameof(Store).Pluralize().Pascalize(), EcommerceDbContext.ECOMMERCE_SCHEMA);
        builder.Property(x => x.MaxUser).IsRequired();
        builder.Property(x => x.StoreName).IsRequired();
        builder.HasMany(x => x.Products).WithOne().OnDelete(DeleteBehavior.Cascade);
        builder.Property(x => x.MaxUser).IsRequired();
    }
}
