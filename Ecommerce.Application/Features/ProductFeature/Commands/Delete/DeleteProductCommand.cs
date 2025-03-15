using Ecommerce.Shared.Wrappers;

namespace Ecommerce.Application.Features.ProductFeature.Commands.Delete;

public record DeleteProductCommand(Guid ProductGuid) : ICommand<BaseResult<Unit>>;
