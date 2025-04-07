namespace Ecommerce.Application.Common.Repository;

public interface IProductRepository : IRepository<Product>
{
    Task<Product?> GetByGuidAsync(Guid productGuid, CancellationToken cancellationToken);
    Task<IEnumerable<Product>> GetAllAsync(long storeId, CancellationToken cancellationToken);
    Task<Product> AddAsync(Product product, CancellationToken cancellationToken);
    void Update(Product product);
    void Delete(Product product);
}

public interface IReadonlyProductRepository : IReadOnlyRepository<Product>
{
    Task<bool> ExistAsync(int id, CancellationToken cancellationToken);
    Task<bool> ExistAsync(Guid productGuid, CancellationToken cancellationToken);
    Task<bool> ExistAsync(
        Guid productGuid,
        Guid productItemGuid,
        CancellationToken cancellationToken
    );
}
