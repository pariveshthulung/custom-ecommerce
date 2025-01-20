namespace Ecommerce.Domain.AggregatesModel.StoreAggregate;

public class Store : AuditableEntity, IAggregateRoot
{
    public new int Id { get; private set; }
    public int MaxUser { get; private set; }
    public string StoreName { get; set; } = default!;
    private List<int> _userId = [];
    private Store(int maxUser, string storeName)
    {
        MaxUser = maxUser;
        StoreName = storeName;
    }
    public static Store Create(int maxUser, string storeName)
    {
        return new Store(maxUser, storeName);
    }

}
