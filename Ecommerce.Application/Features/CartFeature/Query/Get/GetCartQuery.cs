using Ecommerce.Application.Common.Model;

namespace Ecommerce.Application.Features.Carts.Query.Get;

public record GetCartQuery() : IQuery<BaseResult<CartDto>>;
