namespace Ecommerce.Domain.AggregatesModel.StoreAggregate.Entities;

public class Store : AuditableEntity, IAggregateRoot
{
    public int MaxUser { get; private set; }
    public string StoreName { get; set; } = default!;
    private List<Product> _products = [];
    public IReadOnlyCollection<Product> Products => _products.AsReadOnly();
    private List<long> _administratorsIds = [];
    public IReadOnlyCollection<long> AdministratorsIds => _administratorsIds.AsReadOnly();

    private Store(int maxUser, string storeName)
    {
        MaxUser = maxUser;
        StoreName = storeName;
    }

    public static Store Create(int maxUser, string storeName)
    {
        return new Store(maxUser, storeName);
    }

    public void AddAdminstrator(long administratorId)
    {
        _administratorsIds.Add(administratorId);
    }

    public void AddProduct(Product product) => _products.Add(product);
}
