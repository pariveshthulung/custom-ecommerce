namespace Ecommerce.Api.Controllers.Common;

[ApiController]
[Produces("application/json")]
// [ApiExplorerSettings(GroupName = ApiGrouping.EcommerceApiGroupingName)]
[ApiVersion(1.0)]
[EcommerceRoute("")]
public class EcommerceControllerBase(IMapper mapper, ISender sender) : ControllerBase
{
    protected readonly IMapper Mapper = mapper;
    protected readonly ISender Sender = sender;
    protected string UserEmail => User.FindFirst(ClaimTypes.Email)?.Value ?? string.Empty;
    protected string Role => User.FindFirst(ClaimTypes.Role)?.Value ?? string.Empty;
    protected string UserId => User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
}
