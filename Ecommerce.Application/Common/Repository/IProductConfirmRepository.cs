namespace Ecommerce.Application.Common.Repository;

public interface IProductConfirmRepository
{
    Task<ProductConfirm> GetByGuidAsync(
        Guid productConfirmGuid,
        CancellationToken cancellationToken = default(CancellationToken)
    );
    Task<IEnumerable<ProductConfirm>> GetAllAsync(
        CancellationToken cancellationToken = default(CancellationToken)
    );
    Task<ProductConfirm> AddAsync(
        ProductConfirm productConfirm,
        CancellationToken cancellationToken = default(CancellationToken)
    );
    Task<ProductConfirm> UpdateAsync(
        ProductConfirm productConfirm,
        CancellationToken cancellationToken = default(CancellationToken)
    );
    void Delete(
        ProductConfirm productConfirm,
        CancellationToken cancellationToken = default(CancellationToken)
    );
}
