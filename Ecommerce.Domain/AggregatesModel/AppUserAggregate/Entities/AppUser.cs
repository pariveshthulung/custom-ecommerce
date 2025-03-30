namespace Ecommerce.Domain.AggregatesModel.AppUserAggregate.Entities;

//need to inherite from enitity class so  it can add domain event
public class AppUser : IdentityUser<long>, IAggregateRoot
{
    public string FirstName { get; private set; } = default!;
    public string LastName { get; private set; } = default!;
    public string FullName => $"{FirstName} {LastName}";
    public string PhoneNo { get; private set; } = default!;
    public string? RefreshToken { get; private set; }
    public DateTime? RefreshTokenExpiryTime { get; private set; }
    public bool IsPasswordExpire { get; private set; }
    public bool IsDeleted { get; private set; }
    public int RoleId { get; private set; }
    public bool IsActive { get; private set; }
    public Guid? StoreGuid { get; private set; }
    public long? CartId { get; private set; } = default!;
    public Address? Address { get; private set; } = default!;
    private IList<long> _ordersId = [];
    public IReadOnlyCollection<long> OrdersId => _ordersId.AsReadOnly();

    private AppUser(
        string firstName,
        string lastName,
        string email,
        string phoneNo,
        int roleId,
        Guid? storeGuid
    )
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PhoneNo = phoneNo;
        RoleId = roleId;
        UserName = firstName + lastName;
        StoreGuid = storeGuid;
    }

    public static AppUser Create(
        string firstName,
        string lastName,
        string email,
        string phoneNo,
        int roleId,
        Guid? storeGuid
    ) => new(firstName, lastName, email, phoneNo, roleId, storeGuid);

    public void UpdateStoreId(Guid storeGuid) => StoreGuid = storeGuid;
}
