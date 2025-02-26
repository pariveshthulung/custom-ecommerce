using Ecommerce.Domain.Enumerations;

namespace Ecommerce.Infrastructure.EntityConfiguration;

public class UserTypeEnumEntityConfiguration : IEntityTypeConfiguration<UserTypeEnum>
{
    public void Configure(EntityTypeBuilder<UserTypeEnum> builder)
    {
        builder.ToTable(
            nameof(UserTypeEnum).Pluralize().Pascalize(),
            EcommerceDbContext.ECOMMERCE_SCHEMA
        );
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).ValueGeneratedNever().IsRequired();

        builder.Property(x => x.Name).HasMaxLength(200).IsRequired();
    }
}
