using Ecommerce.Shared.Wrappers;

namespace Ecommerce.Application.Features.Carts.Command;

public class AddItemToCartCommandHandler(
    ICartRepository cartRepository,
    ICurrentUserService currentUserService
) : ICommandHandler<AddItemToCartCommand, BaseResult<Guid>>
{
    public async Task<BaseResult<Guid>> Handle(
        AddItemToCartCommand request,
        CancellationToken cancellationToken
    )
    {
        var userCart = await cartRepository.GetByUserIdAsync(
            currentUserService.UserId,
            cancellationToken
        );
        if (userCart == null)
        {
            return await CreateNewCartAsync(currentUserService.UserId, request, cancellationToken);
        }

        return await AddOrUpdateCartItemAsync(userCart, request, cancellationToken);
    }

    private async Task<BaseResult<Guid>> CreateNewCartAsync(
        int userId,
        AddItemToCartCommand request,
        CancellationToken cancellationToken
    )
    {
        var newCart = Cart.Create(userId);
        var newCartItem = CartItem.Create(request.ProductId, request.Quantity);

        newCart.AddCartItem(newCartItem);
        await cartRepository.AddAsync(newCart, cancellationToken);
        await cartRepository.UnitOfWork.SaveEntitiesAsync();

        return BaseResult<Guid>.Ok(newCartItem.Guid);
    }

    private async Task<BaseResult<Guid>> AddOrUpdateCartItemAsync(
        Cart userCart,
        AddItemToCartCommand request,
        CancellationToken cancellationToken
    )
    {
        var existingCartItem = userCart.CartItems.SingleOrDefault(item =>
            item.ProductId == request.ProductId
        );

        if (existingCartItem != null)
        {
            existingCartItem.AddQuantity(request.Quantity);
            await cartRepository.UpdateAsync(userCart, cancellationToken);
            await cartRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            return BaseResult<Guid>.Ok(existingCartItem.Guid);
        }
        else
        {
            var newCartItem = CartItem.Create(request.ProductId, request.Quantity);
            userCart.AddCartItem(newCartItem);
            await cartRepository.UpdateAsync(userCart, cancellationToken);
            await cartRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            return BaseResult<Guid>.Ok(newCartItem.Guid);
        }
    }
}
