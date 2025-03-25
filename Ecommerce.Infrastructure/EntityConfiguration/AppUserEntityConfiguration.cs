namespace Ecommerce.Infrastructure.EntityConfiguration;

public class AppUserEntityConfiguration : IEntityTypeConfiguration<AppUser>
{
    public void Configure(EntityTypeBuilder<AppUser> builder)
    {
        builder.ToTable(
            nameof(AppUser).Pluralize().Pascalize(),
            EcommerceDbContext.ECOMMERCE_SCHEMA
        );
        builder.Property(e => e.FirstName).IsRequired().HasMaxLength(255);
        builder.Property(e => e.LastName).IsRequired().HasMaxLength(255);
        builder.Ignore(e => e.FullName);
        builder.Property(e => e.Email).IsRequired().HasMaxLength(255);
        builder.Property(e => e.PhoneNo).IsRequired().HasMaxLength(255);
        builder.Property(e => e.PasswordHash).IsRequired().HasMaxLength(255);
        builder
            .HasOne<RoleEnum>()
            .WithMany()
            .HasForeignKey(e => e.RoleId)
            .OnDelete(DeleteBehavior.NoAction)
            .IsRequired();
        builder.HasOne<Cart>().WithOne().HasForeignKey<AppUser>(e => e.CartId);

        // builder.HasOne<Store>().WithMany().HasForeignKey(e => e.StoreGuid);
        builder
            .Property(c => c.OrdersId)
            .HasConversion(
                v => string.Join(",", v), // Convert List<long> to a comma-separated string for storage
                v => v.Split(",", StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToList() // Convert string back to List<long>
            );
        builder.OwnsOne(
            e => e.Address,
            address =>
            {
                address.Property(e => e.City).HasColumnName("Customer_City").HasMaxLength(100);
                address
                    .Property(e => e.AddressLine)
                    .HasColumnName("Customer_AddressLine")
                    .HasMaxLength(200);
                address.Property(e => e.StreetNo).HasColumnName("Customer_StreetNo");
                address.Property(e => e.PostalCode).HasColumnName("Customer_PostalCode");
                address.Property(e => e.Region).HasColumnName("Customer_Region");
                address.Property(e => e.IsDefault).HasColumnName("Customer_IsDefault");
            }
        );
    }
}
