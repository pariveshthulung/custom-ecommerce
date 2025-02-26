namespace Ecommerce.Domain.Enumerations;

public class UserTypeEnum(int id, string name) : Enumeration(id, name)
{
    public static UserTypeEnum Customer = new(1, nameof(Customer));
    public static UserTypeEnum SuperAdmin = new(2, nameof(SuperAdmin));
    public static UserTypeEnum Admin = new(3, nameof(Admin));
    public static UserTypeEnum Sales = new(4, nameof(Sales));

    public static List<UserTypeEnum> List() => [Customer, SuperAdmin, Admin, Sales];

    public static List<UserTypeEnum> AdministrativeRoles => [SuperAdmin, Admin];
}
