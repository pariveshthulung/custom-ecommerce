namespace Ecommerce.Api.Controllers;

public class CartController(IMapper mapper, ISender sender, ILogger<CartController> logger)
    : EcommerceControllerBase(mapper, sender)
{
    private const string _swaggerOperationTag = "Cart";

    [HttpGet("cart/{cartGuid:guid}")]
    [SwaggerOperation(
        Summary = "Get Cart",
        Description = "Get Cart",
        OperationId = "Cart.get.cart",
        Tags = [_swaggerOperationTag]
    )]
    [SwaggerResponse(StatusCodes.Status200OK, "Get cart")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Cart not found")]
    [SwaggerResponse(
        StatusCodes.Status500InternalServerError,
        "Application failed to process the request"
    )]
    public async Task<IActionResult> GetCart(Guid cartGuid, CancellationToken cancellationToken)
    {
        try
        {
            var query = new GetCartQuery(cartGuid);
            var response = await Sender.Send(query, cancellationToken);
            return response is null ? NotFound() : Ok(response);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error fetching cart");
            throw;
        }
    }
}
