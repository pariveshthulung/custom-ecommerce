using Ecommerce.Application.Features.ProductFeature.Commands;

namespace Ecommerce.Api.Models;

public record ProductDto : IMapTo<AddProductCommand.Command>
{
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
}
