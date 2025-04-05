using Ecommerce.Infrastructure.EntityConfiguration.Enumerations;
using Ecommerce.Shared.Extension;
using Microsoft.AspNetCore.Identity;
using static Ecommerce.Shared.DomainDesign.Abstraction.Entity;

namespace Ecommerce.Infrastructure.Data;

public class EcommerceDbContext : IdentityDbContext<AppUser, IdentityRole<long>, long>, IUnitOfWork
{
    private readonly DbContextOptions<EcommerceDbContext> options;
    private readonly ILogger<EcommerceDbContext> logger;
    private readonly ICurrentUserService currentUserService;
    private readonly IMediator mediator;

    public EcommerceDbContext(
        DbContextOptions<EcommerceDbContext> options,
        ILogger<EcommerceDbContext> logger,
        ICurrentUserService currentUserService,
        IMediator mediator
    )
        : base(options)
    {
        this.options = options;
        this.logger = logger;
        this.currentUserService = currentUserService;
        this.mediator = mediator;
    }

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
    public DbSet<EventType> EventTypes { get; set; }
    public DbSet<EventLog> EventLogs { get; set; }
    public DbSet<OutBoxMessage> OutBoxMessages { get; set; }

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
            .ApplyConfiguration(new RoleEnumEntityConfiguration())
            .ApplyConfiguration(new EventLogEntityConfiguration())
            .ApplyConfiguration(new EventTypeEntityConfiguration());
    }

    public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            var currentUserId = currentUserService.UserId;
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.AddedBy =
                            entry.Entity.AddedBy <= 0 ? currentUserId : entry.Entity.AddedBy;
                        entry.Entity.AddedOn = DateTime.UtcNow;
                        break;
                    case EntityState.Modified:
                        entry.Entity.UpdatedBy =
                            currentUserId <= 0 ? entry.Entity.UpdatedBy : currentUserId;
                        entry.Entity.UpdatedOn = DateTime.UtcNow;
                        break;
                }
            }
            _EnsureEnumerationUnchanged();
            await base.SaveChangesAsync(cancellationToken);
            // await mediator.DispatchDomainEventAsync(this);
            return true;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);
            throw;
        }
    }

    private void _EnsureEnumerationUnchanged()
    {
        //Enumerations should be changed in the db directly as a short term fix
        foreach (var entry in ChangeTracker.Entries<Enumeration>())
        {
            if (entry.State != EntityState.Unchanged)
            {
                logger.LogInformation(
                    "An attempt to add an enumeration {FullName} entity. This must be manually inserted into the db",
                    entry.Entity.GetType().FullName
                );

                //Updated added state to modified so we can preserve tracking
                if (entry.State == EntityState.Added)
                    entry.State = EntityState.Modified;
                else
                    entry.State = EntityState.Unchanged;
            }
        }
    }
}
