using Ecommerce.Application.Features.UserFeature.Queries;

namespace Ecommerce.Api.Controllers;

public class UserController(IMapper mapper, ISender sender, ILogger<UserController> logger)
    : EcommerceControllerBase(mapper, sender)
{
    private const string _swaggerOperationTag = "user";

    [HttpGet("user/profile")]
    [SwaggerOperation(
        Summary = "User profile",
        Description = "Return current user profile",
        OperationId = "User.Profile",
        Tags = [_swaggerOperationTag]
    )]
    [SwaggerResponse(StatusCodes.Status200OK, "User profile")]
    [SwaggerResponse(
        StatusCodes.Status500InternalServerError,
        "Application failed to process the request"
    )]
    public async Task<IActionResult> GetUserProfile(CancellationToken cancellationToken)
    {
        try
        {
            var response = await Sender.Send(new GetUserProfileQuery.Query(), cancellationToken);
            return Ok(Mapper.Map<UserDto>(response.Data.UserModel));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error getting current user profile");
            throw;
        }
    }
}
