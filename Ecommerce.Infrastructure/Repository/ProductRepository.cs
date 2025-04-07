namespace Ecommerce.Infrastructure.Repository;

public class ProductRepository(
    EcommerceDbContext ecommerceDbContext,
    ILogger<ProductRepository> logger
) : IProductRepository
{
    public IUnitOfWork UnitOfWork => ecommerceDbContext;

    public async Task<Product> AddAsync(Product product, CancellationToken cancellationToken)
    {
        try
        {
            var entity = await ecommerceDbContext.Products.AddAsync(product, cancellationToken);
            return entity.Entity;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error adding product");
            throw;
        }
    }

    public void Delete(Product product)
    {
        try
        {
            ecommerceDbContext.Remove(product);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error removing product");
            throw;
        }
    }

    public async Task<IEnumerable<Product>> GetAllAsync(
        long storeid,
        CancellationToken cancellationToken
    )
    {
        try
        {
            return await ecommerceDbContext
                .Products.Include(x => x.ProductImages)
                .Include(x => x.ProductItems)
                .Where(x => x.StoreId == storeid)
                .ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error fetching products");
            throw;
        }
    }

    public async Task<Product?> GetByGuidAsync(
        Guid productGuid,
        CancellationToken cancellationToken
    )
    {
        try
        {
            return await ecommerceDbContext
                .Products.Include(x => x.ProductImages)
                .Include(x => x.ProductItems)
                .Where(x => x.Guid == productGuid)
                .FirstOrDefaultAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error fetching product");
            throw;
        }
    }

    public void Update(Product product)
    {
        try
        {
            ecommerceDbContext.Update(product);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error updating product");
            throw;
        }
    }
}

public class ReadonlyProductRepository(
    ILogger<ReadonlyProductRepository> logger,
    EcommerceDbContext ecommerceDbContext
) : IReadonlyProductRepository
{
    public async Task<bool> ExistAsync(int id, CancellationToken cancellationToken)
    {
        try
        {
            return await ecommerceDbContext.Products.AnyAsync(x => x.Id == id, cancellationToken);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error checking product");
            throw;
        }
    }

    public async Task<bool> ExistAsync(Guid productGuid, CancellationToken cancellationToken)
    {
        try
        {
            return await ecommerceDbContext.Products.AnyAsync(
                x => x.Guid == productGuid,
                cancellationToken
            );
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error checking product");
            throw;
        }
    }

    public async Task<bool> ExistAsync(
        Guid productGuid,
        Guid productItemGuid,
        CancellationToken cancellationToken
    )
    {
        try
        {
            var product = await ecommerceDbContext
                .Products.Include(x => x.ProductItems)
                .FirstOrDefaultAsync(x => x.Guid == productGuid, cancellationToken);
            if (product is null)
                return false;
            return product.ProductItems.Any(x => x.Guid == productItemGuid);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error checking product item");
            throw;
        }
    }
}
