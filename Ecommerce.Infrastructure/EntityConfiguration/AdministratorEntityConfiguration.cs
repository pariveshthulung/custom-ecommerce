namespace Ecommerce.Infrastructure.EntityConfiguration;

public class AdministratorEntityConfiguration : IEntityTypeConfiguration<Administrator>
{
    public void Configure(EntityTypeBuilder<Administrator> builder)
    {
        builder.Property(e => e.UserTypeId).IsRequired();
        builder.Property(e => e.StoreId).IsRequired();
    }
}
