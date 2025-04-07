namespace Ecommerce.Application.Common.Repository;

public interface ICartRepository : IRepository<Cart>
{
    Task<Cart?> GetByGuidAsync(Guid cartGuid, CancellationToken cancellationToken);
    Task<Cart?> GetByUserIdAsync(long id, CancellationToken cancellationToken);
    Task<IEnumerable<Cart>> GetAllAsync(CancellationToken cancellationToken);
    Task<Cart> AddAsync(Cart cart, CancellationToken cancellationToken);
    void Update(Cart cart);
    void Delete(Cart cart);
}
