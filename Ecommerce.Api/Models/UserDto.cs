using Ecommerce.Application.Common.Model;
using Ecommerce.Domain.Enumerations;

namespace Ecommerce.Api.Models;

public record UserDto : IMapFrom<UserModel>
{
    public Guid UserGuid { get; set; }
    public string Name { get; set; } = default!;
    public long? StoreId { get; set; }
    public RoleEnum Role { get; set; } = default!;
    public required string Email { get; set; }
}
