using Ecommerce.Application.Features.ProductFeature.Commands;

namespace Ecommerce.Api.Controllers;

public class ProductController(IMapper mapper, ISender sender, ILogger<ProductController> logger)
    : EcommerceControllerBase(mapper, sender)
{
    private const string _swaggerOperationTag = "Product";

    [HttpPost("product/add")]
    [SwaggerOperation(
        Summary = "Add new product",
        Description = "Add new product",
        OperationId = "Product.add",
        Tags = [_swaggerOperationTag]
    )]
    [SwaggerResponse(StatusCodes.Status200OK, "Add new product", typeof(Guid))]
    // [SwaggerResponse(StatusCodes.Status404NotFound, "Store not found")]
    [SwaggerResponse(
        StatusCodes.Status500InternalServerError,
        "Application failed to process the request"
    )]
    public async Task<IActionResult> AddProduct(
        [FromBody] ProductDto productDto,
        CancellationToken cancellationToken
    )
    {
        try
        {
            var response = await Sender.Send(
                Mapper.Map<AddProductCommand.Command>(productDto),
                cancellationToken
            );

            return Ok(response);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error fetching store");
            throw;
        }
    }
}
