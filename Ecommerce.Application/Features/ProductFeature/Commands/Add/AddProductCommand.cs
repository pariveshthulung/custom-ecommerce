using Ecommerce.Shared.Wrappers;

namespace Ecommerce.Application.Features.ProductFeature.Commands.Add;

public record AddProductCommand(string Name, string Description) : ICommand<BaseResult<Guid>>;
