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
    [SwaggerResponse(StatusCodes.Status200OK, "Get store", typeof(StoreBase))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Store not found")]
    [SwaggerResponse(
        StatusCodes.Status500InternalServerError,
        "Application failed to process the request"
    )]
    public async Task<IActionResult> GetStore(Guid storeGuid, CancellationToken cancellationToken)
    {
        try
        {
            var response = await Sender.Send(new GetStoreQuery.Query(storeGuid), cancellationToken);

            return Ok(Mapper.Map<StoreBase>(response));
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
            return Created();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error creating store");
            throw;
        }
    }

    [HttpPut("store/{storeGuid:guid}/update")]
    [SwaggerOperation(
        Summary = "Update store",
        Description = "Update store",
        OperationId = "Store.update.name",
        Tags = [_swaggerOperationTag]
    )]
    [SwaggerResponse(StatusCodes.Status200OK, "Updated stored guid", typeof(Guid))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Error creating store")]
    [SwaggerResponse(
        StatusCodes.Status500InternalServerError,
        "Application failed to process the request"
    )]
    public async Task<IActionResult> UpdateStore(
        [FromRoute] Guid storeGuid,
        string storeName,
        CancellationToken cancellationToken
    )
    {
        try
        {
            var command = new UpdateStoreCommand.Command(
                storeGuid,
                storeName,
                AppUserId,
                UserEmail
            );
            var response = await Sender.Send(command, cancellationToken);
            return Ok(response);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error creating store");
            throw;
        }
    }
}
