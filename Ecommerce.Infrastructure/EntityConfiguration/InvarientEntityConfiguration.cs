namespace Ecommerce.Infrastructure.EntityConfiguration;

public class InvarientEntityConfiguration : IEntityTypeConfiguration<Invarient>
{
    public void Configure(EntityTypeBuilder<Invarient> builder)
    {
        builder.ToTable(
            nameof(Invarient).Pluralize().Pascalize(),
            EcommerceDbContext.ECOMMERCE_SCHEMA
        );
        builder.Property(e => e.Name).IsRequired().HasMaxLength(255);
        builder
            .HasMany(e => e.InvarientOptions)
            .WithOne()
            .HasForeignKey(e => e.InvarientId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.Property(e => e.CategoryId).IsRequired();
    }
}
