using Ecommerce.Domain.AggregatesModel.StoreAggregate.Events;

namespace Ecommerce.Domain.AggregatesModel.StoreAggregate.Entities;

public class Store : AuditableEntity, IAggregateRoot
{
    public int MaxUser { get; private set; }
    public string StoreName { get; set; } = default!;
    private List<long> _productsId = [];
    public IReadOnlyCollection<long> ProductsId => _productsId.AsReadOnly();
    private List<long> _administratorsId = [];
    public IReadOnlyCollection<long> AdministratorsId => _administratorsId.AsReadOnly();

    private Store() { }

    private Store(string storeName, long appuserId)
    {
        MaxUser = 3;
        StoreName = storeName;
        AddDomainEvent(new StoreCreatedEvent(appuserId, this.Guid));
    }

    public static Store Create(string storeName, long appuserId)
    {
        return new Store(storeName, appuserId);
    }

    public void AddAdminstrator(long administratorId)
    {
        _administratorsId.Add(administratorId);
    }

    public void AddProduct(long productId)
    {
        _productsId.Add(productId);
    }

    public void Update(string storeName, long appUserId, string userEmail)
    {
        var OldName = StoreName;
        StoreName = storeName;
        AddDomainEvent(new StoreUpdatedEvent(appUserId, userEmail, OldName, this.StoreName));
    }
}
