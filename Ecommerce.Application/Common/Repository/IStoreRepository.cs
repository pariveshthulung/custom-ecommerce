namespace Ecommerce.Application.Common.Repository;

public interface IStoreRepository : IRepository<Store>
{
    Task<Store?> GetByGuidAsync(Guid storeGuid, CancellationToken cancellationToken);
    Task<IEnumerable<Store>> GetAllAsync(CancellationToken cancellationToken);
    Task<Store> AddAsync(Store store, CancellationToken cancellationToken);
    void Update(Store store);
    void Delete(Store store);
}

public interface IReadonlyStoreRepository : IReadOnlyRepository<Store>
{
    Task<bool> Exist(Guid storeGuid, CancellationToken cancellationToken);
}
