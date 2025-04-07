using Ecommerce.Application.Features.ProductFeature.Commands;
using Ecommerce.Application.Features.ProductFeature.Commands.Update;
using Ecommerce.Application.Features.ProductFeature.Queries;

namespace Ecommerce.Api.Controllers;

public class ProductController(IMapper mapper, ISender sender, ILogger<ProductController> logger)
    : EcommerceControllerBase(mapper, sender)
{
    private const string _swaggerOperationTag = "Product";

    [HttpPost("product")]
    [SwaggerOperation(
        Summary = "Add new product",
        Description = "Add new product",
        OperationId = "Product.add",
        Tags = [_swaggerOperationTag]
    )]
    [SwaggerResponse(StatusCodes.Status200OK, "Add new product", typeof(Guid))]
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
            logger.LogError(ex, "Error adding product");
            throw;
        }
    }

    [HttpGet("product")]
    [SwaggerOperation(
        Summary = "Get products",
        Description = "Get list of product",
        OperationId = "Product.get.all.product",
        Tags = [_swaggerOperationTag]
    )]
    [SwaggerResponse(StatusCodes.Status200OK, "Get product")]
    [SwaggerResponse(
        StatusCodes.Status500InternalServerError,
        "Application failed to process the request"
    )]
    public async Task<IActionResult> GetAllProduct(
        long storeId,
        CancellationToken cancellationToken
    )
    {
        try
        {
            var query = new GetAllProductQuery.Query(storeId);
            var response = await Sender.Send(query, cancellationToken);
            return Ok(Mapper.Map<IEnumerable<ProductResponseDto>>(response.Data.ProductModels));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error fetching product");
            throw;
        }
    }

    [HttpGet("product/{productGuid:guid}")]
    [SwaggerOperation(
        Summary = "Get product",
        Description = "Get product",
        OperationId = "Product.get",
        Tags = [_swaggerOperationTag]
    )]
    [SwaggerResponse(StatusCodes.Status200OK, "Get product")]
    [SwaggerResponse(
        StatusCodes.Status500InternalServerError,
        "Application failed to process the request"
    )]
    public async Task<IActionResult> GetProduct(
        Guid productGuid,
        CancellationToken cancellationToken
    )
    {
        try
        {
            var query = new GetProductQuery.Query(productGuid);
            var response = await Sender.Send(query, cancellationToken);
            return Ok(Mapper.Map<ProductResponseDto>(response.Data.ProductModel));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error fetching product");
            throw;
        }
    }

    [HttpPut("product/{productGuid:guid}")]
    [SwaggerOperation(
        Summary = "update product",
        Description = "Update product",
        OperationId = "product.update",
        Tags = [_swaggerOperationTag]
    )]
    [SwaggerResponse(StatusCodes.Status204NoContent, "Update product")]
    [SwaggerResponse(
        StatusCodes.Status500InternalServerError,
        "Application failed to process the request"
    )]
    public async Task<IActionResult> UpdateProduct(
        [FromRoute] Guid productGuid,
        [FromBody] ProductUpdateDto productUpdateDto,
        CancellationToken cancellationToken
    )
    {
        try
        {
            var command = Mapper.Map<UpdateProductCommand.Command>(productUpdateDto);
            command.ProductGuid = productGuid;
            var response = await Sender.Send(command, cancellationToken);
            return NoContent();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error Updating product");
            throw;
        }
    }

    [HttpDelete("product/{productGuid:guid}")]
    [SwaggerOperation(
        Summary = "update product",
        Description = "Update product",
        OperationId = "product.update",
        Tags = [_swaggerOperationTag]
    )]
    [SwaggerResponse(StatusCodes.Status204NoContent, "Update product")]
    [SwaggerResponse(
        StatusCodes.Status500InternalServerError,
        "Application failed to process the request"
    )]
    public async Task<IActionResult> DeleteProduct(
        [FromRoute] Guid productGuid,
        CancellationToken cancellationToken
    )
    {
        try
        {
            var command = new DeleteProductCommand.Command(productGuid);
            var response = await Sender.Send(command, cancellationToken);
            return NoContent();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error deleting product");
            throw;
        }
    }
}
