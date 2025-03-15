namespace Ecommerce.Api.Models;

public record LoginDto() : IMapTo<LoginCommand>
{
    public string Email { get; set; } = default!;
    public string Password { get; set; } = default!;
}

public record RegisterDto : IMapTo<RegisterCommand>
{
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string PhoneNo { get; set; } = default!;
    public string Password { get; set; } = default!;
    public int RoleId { get; set; }
}
