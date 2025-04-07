using Ecommerce.Application.Features.ProductItemFeature.Commands;
using Ecommerce.Application.Features.ProductItemFeature.Queries;

namespace Ecommerce.Api.Controllers;

public class ProductItemController(
    IMapper mapper,
    ISender sender,
    ILogger<ProductController> logger
) : EcommerceControllerBase(mapper, sender)
{
    private const string _swaggerOperationTag = "ProductItem";

    [HttpGet("product/{productGuid:guid}/product-item")]
    [SwaggerOperation(
        Summary = "Get product item list",
        Description = "Get product item",
        OperationId = "Product-item.get-list",
        Tags = [_swaggerOperationTag]
    )]
    [SwaggerResponse(StatusCodes.Status200OK, "Get product item list", typeof(Guid))]
    [SwaggerResponse(
        StatusCodes.Status500InternalServerError,
        "Application failed to process the request"
    )]
    public async Task<IActionResult> GetProductList(
        [FromRoute] Guid productGuid,
        CancellationToken cancellationToken
    )
    {
        try
        {
            var command = new GetProductItemListQuery.Query(productGuid);
            var response = await Sender.Send(command, cancellationToken);
            return Ok(response);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error getting product item list");
            throw;
        }
    }

    [HttpGet("product/{productGuid:guid}/product-item/{productItemGuid:guid}")]
    [SwaggerOperation(
        Summary = "get product item",
        Description = "get product item",
        OperationId = "Product-item.get",
        Tags = [_swaggerOperationTag]
    )]
    [SwaggerResponse(StatusCodes.Status200OK, "get product item", typeof(Guid))]
    [SwaggerResponse(
        StatusCodes.Status500InternalServerError,
        "Application failed to process the request"
    )]
    public async Task<IActionResult> GetProduct(
        [FromRoute] Guid productGuid,
        [FromRoute] Guid productItemGuid,
        CancellationToken cancellationToken
    )
    {
        try
        {
            var command = new GetProductItemQuery.Query(productGuid, productItemGuid);
            var response = await Sender.Send(command, cancellationToken);
            return Ok(response);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error getting product item");
            throw;
        }
    }

    [HttpPost("product/{productGuid:guid}/product-item")]
    [SwaggerOperation(
        Summary = "Add new product item",
        Description = "Add new product item",
        OperationId = "Product-item.add",
        Tags = [_swaggerOperationTag]
    )]
    [SwaggerResponse(StatusCodes.Status200OK, "Add new product item", typeof(Guid))]
    [SwaggerResponse(
        StatusCodes.Status500InternalServerError,
        "Application failed to process the request"
    )]
    public async Task<IActionResult> AddProduct(
        [FromRoute] Guid productGuid,
        [FromBody] ProductItemAddDto productItemAddDto,
        CancellationToken cancellationToken
    )
    {
        try
        {
            var command = Mapper.Map<AddProductItemCommand.Command>(productItemAddDto);
            command.ProductGuid = productGuid;
            var response = await Sender.Send(command, cancellationToken);
            return Ok(response);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error adding product item");
            throw;
        }
    }

    [HttpPut("product/{productGuid:guid}/product-item/{productItemGuid:guid}")]
    [SwaggerOperation(
        Summary = "update product item",
        Description = "update product item",
        OperationId = "Product-item.update",
        Tags = [_swaggerOperationTag]
    )]
    [SwaggerResponse(StatusCodes.Status200OK, "Update product item", typeof(Guid))]
    [SwaggerResponse(
        StatusCodes.Status500InternalServerError,
        "Application failed to process the request"
    )]
    public async Task<IActionResult> UpdateProduct(
        [FromRoute] Guid productGuid,
        [FromRoute] Guid productItemGuid,
        [FromBody] ProductItemUpdateDto productItemUpdateDto,
        CancellationToken cancellationToken
    )
    {
        try
        {
            var command = Mapper.Map<UpdateProductItemCommand.Command>(productItemUpdateDto);
            command.ProductGuid = productGuid;
            command.ProductItemGuid = productItemGuid;
            var response = await Sender.Send(command, cancellationToken);
            return Ok(response);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error updating product item");
            throw;
        }
    }

    [HttpDelete("product/{productGuid:guid}/product-item/{productItemGuid:guid}")]
    [SwaggerOperation(
        Summary = "Delete product item",
        Description = "Delete product item",
        OperationId = "Product-item.delete",
        Tags = [_swaggerOperationTag]
    )]
    [SwaggerResponse(StatusCodes.Status204NoContent, "Delete product item")]
    [SwaggerResponse(
        StatusCodes.Status500InternalServerError,
        "Application failed to process the request"
    )]
    public async Task<IActionResult> DeleteProduct(
        [FromRoute] Guid productGuid,
        [FromRoute] Guid productItemGuid,
        CancellationToken cancellationToken
    )
    {
        try
        {
            var command = new DeleteProductItemCommand.Command(productGuid, productItemGuid);
            var response = await Sender.Send(command, cancellationToken);
            return NoContent();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error deleting product item");
            throw;
        }
    }
}
