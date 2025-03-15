using Ecommerce.Shared.Wrappers;

namespace Ecommerce.Application.Features.Carts.Command.Remove;

public class RemoveCartItemFromCartHandler(
    ICartRepository cartRepository,
    ICurrentUserService currentUserService
) : ICommandHandler<RemoveItemFromCartCommand, BaseResult<Unit>>
{
    public async Task<BaseResult<Unit>> Handle(
        RemoveItemFromCartCommand request,
        CancellationToken cancellationToken
    )
    {
        var cart = await cartRepository.GetByUserIdAsync(
            currentUserService.UserId,
            cancellationToken
        );
        var cartItem = cart?.CartItems.Where(x => x.Id == request.CartItemId).FirstOrDefault();

        if (request.Delete)
            cart?.RemoveCartItem(cartItem!);
        else if (cartItem!.Quantity > 0)
            cartItem.SubtractQuantity(1);

        await cartRepository.UpdateAsync(cart!, cancellationToken);
        await cartRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        return BaseResult<Unit>.Ok(Unit.Value);
    }
}
