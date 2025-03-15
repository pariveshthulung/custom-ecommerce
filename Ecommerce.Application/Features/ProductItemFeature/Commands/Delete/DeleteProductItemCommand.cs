using Ecommerce.Shared.Wrappers;

namespace Ecommerce.Application.Features.ProductItemFeature.Delete;

public record DeleteProductItemCommand(Guid ProductGuid, Guid ProductItemGuid)
    : ICommand<BaseResult<Unit>>;
