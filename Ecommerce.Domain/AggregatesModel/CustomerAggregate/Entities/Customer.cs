namespace Ecommerce.Domain.AggregatesModel.CustomerAggregate.Entities;

public class Customer : AuditableEntity, IAggregateRoot
{
    public new int Id { get; private set; }
    public string FirstName { get; private set; } = default!;
    public string LastName { get; private set; } = default!;
    public string FullName => $"{FirstName} {LastName}";
    public string Email { get; private set; } = default!;
    public string PhoneNo { get; private set; } = default!;
    public string PasswordHash { get; private set; } = default!;
    public UserTypeEnum UserTypeEnum { get; private set; } = default!;
    public Cart Cart { get; private set; } = default!;
    private IList<Order> _orders = [];
    public IReadOnlyCollection<Order> Orders => _orders.AsReadOnly();
    private Customer(
        string firstName,
        string lastName,
        string email,
        string phoneNo,
        string passwordHash,
        UserTypeEnum userTypeEnum)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PhoneNo = phoneNo;
        PasswordHash = passwordHash;
        UserTypeEnum = userTypeEnum;
    }
    public static Customer Create(
        string firstName,
        string lastName,
        string email,
        string phoneNo,
        string passwordHash,
        UserTypeEnum userTypeEnum
    ) => new(firstName, lastName, email, phoneNo, passwordHash, userTypeEnum);
    public void AddOrder(Order order) => _orders.Add(order);
}
