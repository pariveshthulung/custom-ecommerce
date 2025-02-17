using Ecommerce.Application.Common.Repository;
using Ecommerce.Shared.Helper;

namespace Ecommerce.Application.Features.Carts.Command.Remove;

public class RemoveCartItemFromCartHandler
    : ICommandHandler<RemoveItemFromCartCommand, BaseResult<Unit>>
{
    private readonly ICartRepository _cartRepository;
    private readonly ICurrentUserProvider _currentUserProvider;

    public RemoveCartItemFromCartHandler(ICartRepository cartRepository, ICurrentUserProvider userProvider)
    {
        _cartRepository = cartRepository;
        _currentUserProvider = userProvider;
    }
    public async Task<BaseResult<Unit>> Handle(RemoveItemFromCartCommand request, CancellationToken cancellationToken)
    {
        var currentUser = await _currentUserProvider.GetCurrentUser();
        var cart = await _cartRepository.GetByUserIdAsync(currentUser.Id, cancellationToken);
        var cartItem = cart?.CartItems.Where(x => x.Id == request.CartItemId).FirstOrDefault();

        if (request.Delete)
            cart?.RemoveCartItem(cartItem!);
        else
         if (cartItem!.Quantity > 0)
            cartItem.SubtractQuantity(1);

        await _cartRepository.UpdateAsync(cart!, cancellationToken);
        await _cartRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        return BaseResult<Unit>.Ok(Unit.Value);
    }
}
