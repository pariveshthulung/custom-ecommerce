namespace Ecommerce.Api.Controllers;

public class AuthController(ISender sender, ILogger<AuthController> logger, IMapper mapper)
    : EcommerceControllerBase(mapper, sender)
{
    private const string _swaggerOperationTag = "auth";

    [HttpPost("auth/login")]
    [SwaggerOperation(
        Summary = "User authentication",
        Description = "Authenticate user and return token",
        OperationId = "Auth.Login",
        Tags = [_swaggerOperationTag]
    )]
    [SwaggerResponse(StatusCodes.Status200OK, "User authentication", typeof(LoginCommand.Response))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid username and password")]
    [SwaggerResponse(
        StatusCodes.Status500InternalServerError,
        "Application failed to process the request"
    )]
    public async Task<IActionResult> Login(LoginDto loginDto, CancellationToken cancellationToken)
    {
        try
        {
            var command = Mapper.Map<LoginCommand.Command>(loginDto);
            var response = await Sender.Send(command, cancellationToken);
            Response.Cookies.Append(
                "authToken",
                response.Data.accessToken,
                new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict,
                    Expires = DateTime.UtcNow.AddHours(1)
                }
            );
            return Ok(response);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error login in user");
            throw;
        }
    }

    [Authorize]
    [HttpPost("auth/register")]
    [SwaggerOperation(
        Summary = "Register user",
        Description = "Register new user and return id",
        OperationId = "Auth.Register",
        Tags = [_swaggerOperationTag]
    )]
    [SwaggerResponse(StatusCodes.Status201Created, "Register user")]
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
            var command = Mapper.Map<RegisterCommand.Command>(registerDto);
            var response = await Sender.Send(command, cancellationToken);
            return Created();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error Registering new user");
            throw;
        }
    }
}
