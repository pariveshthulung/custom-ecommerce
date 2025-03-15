namespace Ecommerce.Domain.AggregatesModel.AppUserAggregate.Entities;

public class AppUser : IdentityUser<long>, IAggregateRoot
{
    public string FirstName { get; private set; } = default!;
    public string LastName { get; private set; } = default!;
    public string FullName => $"{FirstName} {LastName}";
    public string PhoneNo { get; private set; } = default!;
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiryTime { get; set; }
    public bool IsPasswordExpire { get; set; }
    public bool IsDeleted { get; set; }
    public int RoleId { get; set; }
    public bool IsActive { get; set; }
    public long? StoreId { get; set; }
    public Cart Cart { get; private set; } = default!;
    public Address Address { get; private set; } = default!;
    private IList<Order> _orders = [];
    public IReadOnlyCollection<Order> Orders => _orders.AsReadOnly();

    private AppUser(string firstName, string lastName, string email, string phoneNo, int roleId)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PhoneNo = phoneNo;
        RoleId = roleId;
    }

    public static AppUser Create(
        string firstName,
        string lastName,
        string email,
        string phoneNo,
        int roleId
    ) => new(firstName, lastName, email, phoneNo, roleId);

    public void AddOrder(Order order) => _orders.Add(order);
}
