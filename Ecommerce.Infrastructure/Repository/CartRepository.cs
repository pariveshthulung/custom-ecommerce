namespace Ecommerce.Infrastructure.Repository;

public class CartRepository(ILogger<CartRepository> logger, EcommerceDbContext ecommerceDbContext)
    : ICartRepository
{
    public IUnitOfWork UnitOfWork => ecommerceDbContext;

    public async Task<Cart> AddAsync(Cart cart, CancellationToken cancellationToken = default)
    {
        try
        {
            var entity = await ecommerceDbContext.AddAsync(cart, cancellationToken);
            return entity.Entity;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error adding cart");
            throw;
        }
    }

    public void Delete(Cart cart)
    {
        try
        {
            ecommerceDbContext.Carts.Remove(cart);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error removing cart");
            throw;
        }
    }

    public async Task<IEnumerable<Cart>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            return await ecommerceDbContext.Carts.ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error fetching carts");
            throw;
        }
    }

    public async Task<Cart?> GetByGuidAsync(
        Guid cartGuid,
        CancellationToken cancellationToken = default
    )
    {
        try
        {
            return await ecommerceDbContext.Carts.FirstOrDefaultAsync(
                x => x.Guid == cartGuid,
                cancellationToken
            );
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error fetching cart");
            throw;
        }
    }

    public async Task<Cart?> GetByUserIdAsync(long userId, CancellationToken cancellationToken)
    {
        try
        {
            return await ecommerceDbContext.Carts.FirstOrDefaultAsync(
                x => x.AppUserId == userId,
                cancellationToken
            );
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error fetching cart");
            throw;
        }
    }

    public void Update(Cart cart)
    {
        try
        {
            ecommerceDbContext.Update(cart);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error updating cart");
            throw;
        }
    }
}
