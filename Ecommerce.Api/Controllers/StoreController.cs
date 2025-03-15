namespace Ecommerce.Api.Controllers;

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
}
