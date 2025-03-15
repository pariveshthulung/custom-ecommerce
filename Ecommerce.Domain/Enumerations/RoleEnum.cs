namespace Ecommerce.Domain.Enumerations;

public class RoleEnum(int id, string name) : Enumeration(id, name)
{
    public static RoleEnum Customer = new(1, nameof(Customer));
    public static RoleEnum SuperAdmin = new(2, nameof(SuperAdmin));
    public static RoleEnum Admin = new(3, nameof(Admin));
    public static RoleEnum Sales = new(4, nameof(Sales));

    public static List<RoleEnum> List() => [Customer, SuperAdmin, Admin, Sales];

    public static List<RoleEnum> AdministrativeRoles => [SuperAdmin, Admin];
}
