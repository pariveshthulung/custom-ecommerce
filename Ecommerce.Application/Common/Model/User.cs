namespace Ecommerce.Application.Common.Model;

public class User
{
    public long Id { get; set; }
    public required string Name { get; set; }
    public long? StoreId { get; set; }
    public required string Role { get; set; }
    public required string Email { get; set; }
}
