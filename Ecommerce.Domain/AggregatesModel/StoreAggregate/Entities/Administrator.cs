namespace Ecommerce.Domain.AggregatesModel.StoreAggregate.Entities;

public class Administrator : AuditableEntity
{
    public string? Name { get; private set; }
    public int UserTypeId { get; set; } = default!;
    public long StoreId { get; private set; }

    private Administrator(string name, long storeId, int userTypeId)
    {
        Name = Guard.Against.NullOrWhiteSpace(name);
        StoreId = Guard.Against.NegativeOrZero(storeId);
        UserTypeId = userTypeId;
    }

    public static Administrator Create(string name, long storeId, int userTypeId) =>
        new(name, storeId, userTypeId);
}
