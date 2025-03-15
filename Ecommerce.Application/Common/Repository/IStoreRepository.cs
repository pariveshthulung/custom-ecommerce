namespace Ecommerce.Application.Common.Repository;

public interface IStoreRepository
{
    Task<Store> GetByGuidAsync(
        Guid storeGuid,
        CancellationToken cancellationToken = default(CancellationToken)
    );
    Task<IEnumerable<Store>> GetAllAsync(
        CancellationToken cancellationToken = default(CancellationToken)
    );
    Task<Store> AddAsync(
        Store store,
        CancellationToken cancellationToken = default(CancellationToken)
    );
    Task<Store> UpdateAsync(
        Store store,
        CancellationToken cancellationToken = default(CancellationToken)
    );
    void Delete(Store store, CancellationToken cancellationToken = default(CancellationToken));
}
