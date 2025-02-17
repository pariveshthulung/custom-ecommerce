namespace Ecommerce.Infrastructure.Repository;

public class StoreRepository : IStoreRepository
{
    public Task<Store> AddAsync(Store store, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public void Delete(Store store, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Store>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Store> GetByGuidAsync(Guid storeGuid, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Store> UpdateAsync(Store store, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
