using Ecommerce.Application.Features.StoreFeature.Command;
using Microsoft.AspNetCore.Authorization;

namespace Ecommerce.Api.Controllers;

[Authorize]
public class StoreController(IMapper mapper, ISender sender, ILogger<StoreController> logger)
    : EcommerceControllerBase(mapper, sender)
{
    private const string _swaggerOperationTag = "Store";

    [HttpGet("store/{storeGuid:guid}")]
    [SwaggerOperation(
        Summary = "Get store",
        Description = "Get store",
        OperationId = "Store.get.store",
        Tags = [_swaggerOperationTag]
    )]
    [SwaggerResponse(StatusCodes.Status200OK, "Get store")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Store not found")]
    [SwaggerResponse(
        StatusCodes.Status500InternalServerError,
        "Application failed to process the request"
    )]
    public IActionResult GetStore(Guid storeGuid)
    {
        try
        {
            return Ok();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error fetching store");
            throw;
        }
    }

    [HttpPost("store/create")]
    [SwaggerOperation(
        Summary = "Create store",
        Description = "Create store",
        OperationId = "Store.create.store",
        Tags = [_swaggerOperationTag]
    )]
    [SwaggerResponse(StatusCodes.Status201Created, "Create store")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Error creating store")]
    [SwaggerResponse(
        StatusCodes.Status500InternalServerError,
        "Application failed to process the request"
    )]
    public async Task<IActionResult> CreateStore(
        string storeName,
        CancellationToken cancellationToken
    )
    {
        try
        {
            var command = new CreateStoreCommand.Command(storeName, AppUserId);
            var response = await Sender.Send(command, cancellationToken);
            return response.Success ? Created() : response.ToProblemDetail();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error creating store");
            throw;
        }
    }
}
