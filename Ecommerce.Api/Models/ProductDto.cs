using Ecommerce.Application.Common.Model;
using Ecommerce.Application.Features.ProductFeature.Commands;
using Ecommerce.Application.Features.ProductFeature.Commands.Update;

namespace Ecommerce.Api.Models;

public record ProductDtoBase
{
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
}

public record ProductUpdateDto : ProductDtoBase, IMapTo<UpdateProductCommand.Command>;

public record ProductDto : ProductDtoBase, IMapTo<AddProductCommand.Command>;

public record ProductResponseDto : IMapFrom<ProductModel>
{
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public long StoreId { get; set; }
    public IEnumerable<ProductImageDto> ProductImages { get; set; } = default!;
    public IEnumerable<ProductItemDto> ProductItems { get; set; } = default!;
}
