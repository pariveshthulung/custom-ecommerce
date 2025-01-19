namespace Ecommerce.Shared.DomainDesign.Abstraction;

public interface IUnitOfWork
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default);
    IUnitOfWork Reset => this;
}
