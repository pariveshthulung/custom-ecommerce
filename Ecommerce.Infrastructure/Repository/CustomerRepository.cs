namespace Ecommerce.Infrastructure.Repository;

public class CustomerRepository : ICustomerRepository
{
    public IUnitOfWork UnitOfWork => throw new NotImplementedException();

    public Task<Customer> AddAsync(Customer customer, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public void Delete(Customer customer, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Customer>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Customer> GetByGuidAsync(Guid customerGuid, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Customer> UpdateAsync(Customer customer, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
