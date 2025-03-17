namespace Ecommerce.Application.Common.Repository;

public interface ISeederRepository
{
    Task<int> SeedRole();
    Task<int> SeedAdministrator();
}
