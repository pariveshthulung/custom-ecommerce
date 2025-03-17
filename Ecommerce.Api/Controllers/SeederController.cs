namespace Ecommerce.Api.Controllers;

public class SeederController(
    ISender sender,
    // ILogger<AuthController> logger,
    IMapper mapper
) : EcommerceControllerBase(mapper, sender)
{
    private const string _swaggerOperationTag = "Seeder";

    [HttpPost("seeder/role")]
    [SwaggerOperation(
        Summary = "seed role",
        Description = "seed role",
        OperationId = "seed.role",
        Tags = [_swaggerOperationTag]
    )]
    [SwaggerResponse(StatusCodes.Status200OK, "seed role")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Can not seed role")]
    [SwaggerResponse(
        StatusCodes.Status500InternalServerError,
        "Application failed to process the request"
    )]
    public async Task<IActionResult> SeedRole(CancellationToken cancellationToken)
    {
        try
        {
            var response = await Sender.Send(new SeedRole.Command(), cancellationToken);
            return response.Success ? Ok(response) : response.ToProblemDetail();
        }
        catch (Exception ex)
        {
            // logger.LogError(ex, "Error login in user");
            throw;
        }
    }

    [HttpPost("seeder/administrator")]
    [SwaggerOperation(
        Summary = "seed administrator",
        Description = "seed administrator",
        OperationId = "seed.administrator",
        Tags = [_swaggerOperationTag]
    )]
    [SwaggerResponse(StatusCodes.Status200OK, "seed administrator")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Can not seed administrator")]
    [SwaggerResponse(
        StatusCodes.Status500InternalServerError,
        "Application failed to process the request"
    )]
    public async Task<IActionResult> SeedAdministrator(CancellationToken cancellationToken)
    {
        try
        {
            var response = await Sender.Send(new SeedAdministrator.Command(), cancellationToken);
            return response.Success ? Ok(response) : response.ToProblemDetail();
        }
        catch (Exception ex)
        {
            // logger.LogError(ex, "Error login in user");
            throw;
        }
    }
}
