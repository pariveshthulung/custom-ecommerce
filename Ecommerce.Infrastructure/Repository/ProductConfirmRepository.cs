namespace Ecommerce.Infrastructure.Repository;

public class ProductConfirmRepository : IProductConfirmRepository
{
    public Task<ProductConfirm> AddAsync(ProductConfirm productConfirm, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public void Delete(ProductConfirm productConfirm, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<ProductConfirm>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<ProductConfirm> GetByGuidAsync(Guid productConfirmGuid, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<ProductConfirm> UpdateAsync(ProductConfirm productConfirm, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
