namespace Ecommerce.Application.Common.Repository;

public interface IProductRepository : IRepository<Product>
{
    Task<Product> GetByGuidAsync(Guid productGuid, CancellationToken cancellationToken = default(CancellationToken));
    Task<IEnumerable<Product>> GetAllAsync(CancellationToken cancellationToken = default(CancellationToken));
    Task<Product> AddAsync(Product product, CancellationToken cancellationToken = default(CancellationToken));
    Task<Product> UpdateAsync(Product product, CancellationToken cancellationToken = default(CancellationToken));
    void Delete(Product product);
    Task<bool> ExistAsync(int id, CancellationToken cancellationToken = default(CancellationToken));


}
