namespace Ecommerce.Application.Common.Model;

public record UserModel : IMapFrom<AppUser>
{
    public Guid UserGuid { get; set; }
    public string Name { get; set; } = default!;
    public long? StoreId { get; set; }
    public RoleEnum Role { get; set; } = default!;
    public required string Email { get; set; }

    public void Mapping(Profile profile)
    {
        profile
            .CreateMap<AppUser, UserModel>()
            .ForMember(_ => _.Role, a => a.MapFrom(_ => Enumeration.FromValue<RoleEnum>(_.RoleId)))
            .ForMember(_ => _.Name, a => a.MapFrom(_ => $"{_.FirstName}  {_.LastName}"));
    }
}
