using Ecommerce.Domain.AggregatesModel.CartAggregate.Entities;

namespace Ecommerce.Application.Features.Cart.Command;

public record AddItemToCartCommand(Guid UserId, CartItem CartItem) : ICommand<BaseResult<Guid>>;
