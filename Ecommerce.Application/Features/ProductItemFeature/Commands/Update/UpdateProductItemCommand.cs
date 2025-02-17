namespace Ecommerce.Application.Features.ProductItemFeature.Update;

public record UpdateProductItemCommand(Guid ProductGuid,
    Guid ProductItemGuid,
    string Image,
    string Sku,
    int Quantity,
    decimal Price)
    : ICommand<BaseResult<Guid>>;
