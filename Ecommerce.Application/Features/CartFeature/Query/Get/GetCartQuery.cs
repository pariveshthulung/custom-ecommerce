using Ecommerce.Shared.Wrappers;

namespace Ecommerce.Application.Features.Carts.Query.Get;

public record GetCartQuery(Guid CartGuid) : IQuery<BaseResult<CartDto>>;
