namespace Ecommerce.Shared.DomainDesign.Abstraction;

public interface IRepository<T>
    where T : IQuerableEntity
{
    IUnitOfWork UnitOfWork { get; }
}

public interface IReadOnlyRepository<T>
    where T : IQuerableEntity;
