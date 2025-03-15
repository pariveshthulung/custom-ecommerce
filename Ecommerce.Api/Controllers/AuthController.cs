using Ecommerce.Shared.Extension;

namespace Ecommerce.Api.Controllers;

public class AuthController(
    ISender sender,
    // ILogger<AuthController> logger,
    IMapper mapper
) : EcommerceControllerBase(mapper, sender)
{
    private const string _swaggerOperationTag = "auth";

    [HttpPost("auth/login")]
    [SwaggerOperation(
        Summary = "Auth login",
        Description = "Auth login",
        OperationId = "Auth.Login",
        Tags = [_swaggerOperationTag]
    )]
    [SwaggerResponse(StatusCodes.Status200OK, "Auth login")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid username and password")]
    [SwaggerResponse(
        StatusCodes.Status500InternalServerError,
        "Application failed to process the request"
    )]
    public async Task<IActionResult> Login(LoginDto loginDto, CancellationToken cancellationToken)
    {
        try
        {
            var command = Mapper.Map<LoginCommand>(loginDto);
            var response = await Sender.Send(command, cancellationToken);
            return response.Success ? Ok(response) : response.ToProblemDetail();
        }
        catch (Exception ex)
        {
            // logger.LogError(ex, "Error login in user");
            throw;
        }
    }

    [HttpPost("auth/register")]
    [SwaggerOperation(
        Summary = "Auth register",
        Description = "Auth register",
        OperationId = "Auth.Register",
        Tags = [_swaggerOperationTag]
    )]
    [SwaggerResponse(StatusCodes.Status201Created, "Auth register")]
    [SwaggerResponse(StatusCodes.Status400BadRequest)]
    [SwaggerResponse(
        StatusCodes.Status500InternalServerError,
        "Application failed to process the request"
    )]
    public async Task<IActionResult> Register(
        RegisterDto registerDto,
        CancellationToken cancellationToken
    )
    {
        try
        {
            var command = Mapper.Map<RegisterCommand>(registerDto);
            var response = await Sender.Send(command, cancellationToken);
            return response.Success ? Created() : response.ToProblemDetail();
        }
        catch (Exception ex)
        {
            // logger.LogError(ex, "Error login in user");
            throw;
        }
    }
}
