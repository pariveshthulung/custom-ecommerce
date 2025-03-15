using Microsoft.AspNetCore.Identity;

namespace Ecommerce.Infrastructure.Data;

public class EcommerceDbContext : IdentityDbContext<AppUser, IdentityRole<long>, long>
{
    public EcommerceDbContext(DbContextOptions<EcommerceDbContext> options)
        : base(options) { }

    public const string ECOMMERCE_SCHEMA = "ecom";
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductImage> ProductImages { get; set; }
    public DbSet<ProductItem> ProductItems { get; set; }
    public DbSet<Cart> Carts { get; set; }
    public DbSet<CartItem> CartItems { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Invarient> Invarients { get; set; }
    public DbSet<InvarientOption> InvarientOptions { get; set; }
    public DbSet<AppUser> AppUsers { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<ProductConfirm> ProductConfirms { get; set; }
    public DbSet<Store> Stores { get; set; }
    public DbSet<RoleEnum> RoleEnums { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder
            .ApplyConfiguration(new CartEntityConfiguration())
            .ApplyConfiguration(new CartItemEntityConfiguration())
            .ApplyConfiguration(new CategoryEntityConfiguration())
            .ApplyConfiguration(new AppUserEntityConfiguration())
            .ApplyConfiguration(new InvarientEntityConfiguration())
            .ApplyConfiguration(new InvarientOptionEntityConfiguration())
            .ApplyConfiguration(new OrderEntityConfiguration())
            .ApplyConfiguration(new OrderItemEntityConfiguration())
            .ApplyConfiguration(new ProductConfirmEntityConfiguration())
            .ApplyConfiguration(new ProductEntityConfiguration())
            .ApplyConfiguration(new ProductImageEntityConfiguration())
            .ApplyConfiguration(new StoreEntityConfiguration())
            .ApplyConfiguration(new ProductItemEntityConfiguration())
            .ApplyConfiguration(new RoleEnumEntityConfiguration());
    }
}
