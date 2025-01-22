namespace Ecommerce.Domain.AggregatesModel.StoreAggregate.Entities;

public class Administrator : AuditableEntity
{
    public new int Id { get; private set; }
    public string? Name { get; private set; }
    public UserTypeEnum UserType { get; set; } = default!;
    public int StoreId { get; private set; }
    private Administrator(string name, int storeId, UserTypeEnum userType)
    {
        Name = Guard.Against.NullOrWhiteSpace(name);
        StoreId = Guard.Against.NegativeOrZero(storeId);
        UserType = userType;
    }
    public static Administrator Create(string name, int storeId, UserTypeEnum userType)
        => new(name, storeId, userType);
}
