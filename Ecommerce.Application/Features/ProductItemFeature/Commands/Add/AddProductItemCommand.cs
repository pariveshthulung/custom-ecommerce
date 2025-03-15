using Ecommerce.Shared.Wrappers;

namespace Ecommerce.Application.Features.ProductItemFeature.Add;

public record AddProductItemCommand(
    Guid ProductGuid,
    string Image,
    string Sku,
    int Quantity,
    decimal Price
) : ICommand<BaseResult<Guid>>;
