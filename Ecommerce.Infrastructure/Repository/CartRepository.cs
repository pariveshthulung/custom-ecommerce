namespace Ecommerce.Infrastructure.Repository;

public class CartRepository : ICartRepository
{
    public IUnitOfWork UnitOfWork => throw new NotImplementedException();

    public Task<Cart> AddAsync(Cart cart, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public void Delete(Cart cart, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Cart>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Cart?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Cart> GetByGuidAsync(Guid orderGuid, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Cart> GetByUserIdAsync(int userId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Cart> UpdateAsync(Cart cart, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
