namespace Ecommerce.Application.Common.Repository;

public interface ICartRepository : IRepository<Cart>
{
    Task<Cart> GetByGuidAsync(
        Guid orderGuid,
        CancellationToken cancellationToken = default(CancellationToken)
    );
    Task<Cart> GetByUserIdAsync(
        long id,
        CancellationToken cancellationToken = default(CancellationToken)
    );
    Task<Cart?> GetByEmailAsync(
        string email,
        CancellationToken cancellationToken = default(CancellationToken)
    );
    Task<IEnumerable<Cart>> GetAllAsync(
        CancellationToken cancellationToken = default(CancellationToken)
    );
    Task<Cart> AddAsync(
        Cart cart,
        CancellationToken cancellationToken = default(CancellationToken)
    );
    Task<Cart> UpdateAsync(
        Cart cart,
        CancellationToken cancellationToken = default(CancellationToken)
    );
    void Delete(Cart cart, CancellationToken cancellationToken = default(CancellationToken));
}
