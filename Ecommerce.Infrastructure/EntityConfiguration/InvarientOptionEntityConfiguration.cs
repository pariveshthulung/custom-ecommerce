namespace Ecommerce.Infrastructure.EntityConfiguration;

public class InvarientOptionEntityConfiguration : IEntityTypeConfiguration<InvarientOption>
{
    public void Configure(EntityTypeBuilder<InvarientOption> builder)
    {
        builder.ToTable(
            nameof(InvarientOption).Pluralize().Pascalize(),
            EcommerceDbContext.ECOMMERCE_SCHEMA
        );
        builder.Property(e => e.Value).IsRequired().HasMaxLength(255);
        builder.Property(e => e.InvarientId).IsRequired();
    }
}
