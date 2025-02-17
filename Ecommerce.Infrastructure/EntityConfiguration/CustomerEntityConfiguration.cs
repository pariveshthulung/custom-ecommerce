using Ecommerce.Domain.AggregatesModel.CustomerAggregate;
using Ecommerce.Domain.Enumerations;

namespace Ecommerce.Infrastructure.EntityConfiguration;

public class CustomerEntityConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.ToTable(
            nameof(Customer).Pluralize().Pascalize(),
            EcommerceDbContext.ECOMMERCE_SCHEMA
        );
        builder.Property(e => e.FirstName).IsRequired().HasMaxLength(255);
        builder.Property(e => e.LastName).IsRequired().HasMaxLength(255);
        builder.Ignore(e => e.FullName);
        builder.Property(e => e.Email).IsRequired().HasMaxLength(255);
        builder.Property(e => e.PhoneNo).IsRequired().HasMaxLength(255);
        builder.Property(e => e.PasswordHash).IsRequired().HasMaxLength(255);
        builder
            .HasOne<UserTypeEnum>()
            .WithMany()
            .HasForeignKey(e => e.UserTypeId)
            .OnDelete(DeleteBehavior.NoAction)
            .IsRequired();
        builder.HasOne(e => e.Cart).WithOne().HasForeignKey<Cart>(e => e.UserId).IsRequired();
        builder
            .HasMany(e => e.Orders)
            .WithOne()
            .HasForeignKey(e => e.CustomerId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.OwnsOne(
            e => e.Address,
            address =>
            {
                address
                    .Property(e => e.City)
                    .HasColumnName("Customer_City")
                    .HasMaxLength(100)
                    .IsRequired();
                address
                    .Property(e => e.AddressLine)
                    .HasColumnName("Customer_AddressLine")
                    .HasMaxLength(200)
                    .IsRequired();
                address.Property(e => e.StreetNo).HasColumnName("Customer_StreetNo").IsRequired();
                address
                    .Property(e => e.PostalCode)
                    .HasColumnName("Customer_PostalCode")
                    .IsRequired();
                address.Property(e => e.Region).HasColumnName("Customer_Region").IsRequired();
                address.Property(e => e.IsDefault).HasColumnName("Customer_IsDefault").IsRequired();
            }
        );
    }
}
