namespace Ecommerce.Infrastructure.EntityConfiguration;

public class RoleEnumEntityConfiguration : IEntityTypeConfiguration<RoleEnum>
{
    public void Configure(EntityTypeBuilder<RoleEnum> builder)
    {
        builder.ToTable(
            nameof(RoleEnum).Pluralize().Pascalize(),
            EcommerceDbContext.ECOMMERCE_SCHEMA
        );
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).ValueGeneratedNever().IsRequired();

        builder.Property(x => x.Name).HasMaxLength(200).IsRequired();
    }
}
