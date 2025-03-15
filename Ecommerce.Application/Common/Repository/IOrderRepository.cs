namespace Ecommerce.Application.Common.Repository;

public interface IOrderRepository : IRepository<Order>
{
    Task<IEnumerable<Order>> GetAllAsync(
        CancellationToken cancellationToken = default(CancellationToken)
    );
    Task<Order> GetByGuidAsync(
        Guid orderGuid,
        CancellationToken cancellationToken = default(CancellationToken)
    );
    Task<Order> AddAsync(
        Order order,
        CancellationToken cancellationToken = default(CancellationToken)
    );
    Task<Order> UpdateAsync(
        Order order,
        CancellationToken cancellationToken = default(CancellationToken)
    );
    void DeleteAsync(Order order, CancellationToken cancellationToken = default(CancellationToken));
}

public interface IOrderReadOnlyRepository : IReadOnlyRepository<Order>
{
    Task<IEnumerable<Order>> GetAllAsync(
        CancellationToken cancellationToken = default(CancellationToken)
    );
    Task<Order> GetByGuidAsync(
        Guid orderGuid,
        CancellationToken cancellationToken = default(CancellationToken)
    );
}
