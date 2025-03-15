namespace Ecommerce.Shared.Model;

public class CurrentUser
{
    public int Id { get; set; }
    public Guid Guid { get; set; }
    public string Name { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string UserType { get; set; } = default!;
}
