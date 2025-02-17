using Ecommerce.Application.Common.Repository;
using FluentValidation;

namespace Ecommerce.Application.Features.Carts.Command.Remove;

public record RemoveItemFromCartCommand(int CartItemId, bool Delete = false) : ICommand<BaseResult<Unit>>;

public class RemoveItemFromCartCommandValidator : AbstractValidator<RemoveItemFromCartCommand>
{
    public RemoveItemFromCartCommandValidator(ICartRepository cartRepository)
    {
        RuleFor(x => x.CartItemId)
        .NotNull()
        .NotEmpty()
        .WithMessage("Invalid cart item.");

        RuleFor(x => x.CartItemId)
        .MustAsync(async (id, cancellationToken) =>
        {
            var cart = await cartRepository.GetAllAsync(cancellationToken);
            var cartItem = cart.SelectMany(x => x.CartItems).FirstOrDefault(x => x.Id == id);
            return cartItem != null;
        })
        .WithMessage("Invalid cart item.");
    }
}
