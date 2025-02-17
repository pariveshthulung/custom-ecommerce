namespace Ecommerce.Domain.AggregatesModel.CustomerAggregate.Entities;

public class Customer : AuditableEntity, IAggregateRoot
{
    public string FirstName { get; private set; } = default!;
    public string LastName { get; private set; } = default!;
    public string FullName => $"{FirstName} {LastName}";
    public string Email { get; private set; } = default!;
    public string PhoneNo { get; private set; } = default!;
    public string PasswordHash { get; private set; } = default!;
    public int UserTypeId { get; private set; }
    public Cart Cart { get; private set; } = default!;
    public Address Address { get; private set; }
    private IList<Order> _orders = [];
    public IReadOnlyCollection<Order> Orders => _orders.AsReadOnly();

    private Customer(
        string firstName,
        string lastName,
        string email,
        string phoneNo,
        string passwordHash,
        int userTypeId
    )
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PhoneNo = phoneNo;
        PasswordHash = passwordHash;
        UserTypeId = userTypeId;
    }

    public static Customer Create(
        string firstName,
        string lastName,
        string email,
        string phoneNo,
        string passwordHash,
        int userTypeEnum
    ) => new(firstName, lastName, email, phoneNo, passwordHash, userTypeEnum);

    public void AddOrder(Order order) => _orders.Add(order);
}
