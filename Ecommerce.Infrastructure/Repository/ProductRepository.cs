namespace Ecommerce.Infrastructure.Repository;

public class ProductRepository(EcommerceDbContext ecommerceDbContext) : IProductRepository
{
    public IUnitOfWork UnitOfWork => ecommerceDbContext;

    public async Task<Product> AddAsync(
        Product product,
        CancellationToken cancellationToken = default
    )
    {
        try
        {
            var entity = await ecommerceDbContext.Products.AddAsync(product);
            return entity.Entity;
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public void Delete(Product product)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ExistAsync(int id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Product>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Product> GetByGuidAsync(
        Guid productGuid,
        CancellationToken cancellationToken = default
    )
    {
        throw new NotImplementedException();
    }

    public Task<Product> UpdateAsync(Product product, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
