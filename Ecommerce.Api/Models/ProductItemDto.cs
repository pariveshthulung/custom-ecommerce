using Ecommerce.Application.Common.Model;
using Ecommerce.Application.Features.ProductItemFeature.Commands;

namespace Ecommerce.Api.Models;

public record ProductItemBaseDto
{
    public string Image { get; set; } = default!;
    public string SKU { get; set; } = default!;
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}

public record ProductItemDto : ProductItemBaseDto, IMapFrom<ProductItemModel>
{
    public Guid Guid { get; set; }
    public long ProductId { get; set; }
}

public record ProductItemAddDto : ProductItemBaseDto, IMapTo<AddProductItemCommand.Command>;

public record ProductItemUpdateDto : ProductItemBaseDto, IMapTo<AddProductItemCommand.Command>;
