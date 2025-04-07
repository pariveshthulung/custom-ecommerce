namespace Ecommerce.Shared.DomainDesign.Abstraction;

public interface IHasDomainEvents
{
    IReadOnlyCollection<INotification> DomainEvent { get; }
    void AddDomainEvent(INotification domainEvent);
    void RemoveDomainEvent(INotification domainEvent);
    void ClearDomainEvent();
}
