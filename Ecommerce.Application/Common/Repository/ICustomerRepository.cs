namespace Ecommerce.Application.Common.Repository;

public interface ICustomerRepository : IRepository<Customer>
{
    Task<Customer> GetByGuidAsync(Guid customerGuid, CancellationToken cancellationToken = default(CancellationToken));
    Task<IEnumerable<Customer>> GetAllAsync(CancellationToken cancellationToken = default(CancellationToken));
    Task<Customer> AddAsync(Customer customer, CancellationToken cancellationToken = default(CancellationToken));
    Task<Customer> UpdateAsync(Customer customer, CancellationToken cancellationToken = default(CancellationToken));
    void Delete(Customer customer, CancellationToken cancellationToken = default(CancellationToken));

}
