namespace Ecommerce.Infrastructure.Repository;

public class OrderRepository : IOrderRepository
{
    public IUnitOfWork UnitOfWork => throw new NotImplementedException();

    public Task<Order> AddAsync(Order order, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public void DeleteAsync(Order order, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Order>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Order> GetByGuidAsync(Guid orderGuid, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Order> UpdateAsync(Order order, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
