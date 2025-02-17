
using Ecommerce.Application.Common.Repository;
using Ecommerce.Shared.Helper;

namespace Ecommerce.Application.Features.Carts.Command;

public class AddItemToCartCommandHandler
    : ICommandHandler<AddItemToCartCommand, BaseResult<Guid>>
{
    private readonly ICartRepository _cartItemRepository;
    private readonly ICurrentUserProvider _currentUserProvider;
    public AddItemToCartCommandHandler(ICartRepository cartRepository, ICurrentUserProvider currentUserProvider)
    {
        _cartItemRepository = cartRepository;
        _currentUserProvider = currentUserProvider;
    }
    public async Task<BaseResult<Guid>> Handle(
      AddItemToCartCommand request,
      CancellationToken cancellationToken)
    {
        var currentUser = _currentUserProvider.GetCurrentUser();
        var userCart = await _cartItemRepository.GetByUserIdAsync(currentUser.Id, cancellationToken);
        if (userCart == null)
        {
            return await CreateNewCartAsync(currentUser.Id, request, cancellationToken);
        }

        return await AddOrUpdateCartItemAsync(userCart, request, cancellationToken);
    }

    private async Task<BaseResult<Guid>> CreateNewCartAsync(
        int userId,
        AddItemToCartCommand request,
        CancellationToken cancellationToken)
    {
        var newCart = Cart.Create(userId);
        var newCartItem = CartItem.Create(request.ProductId, request.Quantity);

        newCart.AddCartItem(newCartItem);
        await _cartItemRepository.AddAsync(newCart, cancellationToken);
        await _cartItemRepository.UnitOfWork.SaveEntitiesAsync();

        return BaseResult<Guid>.Ok(newCartItem.Guid);
    }

    private async Task<BaseResult<Guid>> AddOrUpdateCartItemAsync(
        Cart userCart,
        AddItemToCartCommand request,
        CancellationToken cancellationToken)
    {
        var existingCartItem = userCart.CartItems
            .SingleOrDefault(item => item.ProductId == request.ProductId);

        if (existingCartItem != null)
        {
            existingCartItem.AddQuantity(request.Quantity);
            await _cartItemRepository.UpdateAsync(userCart, cancellationToken);
            await _cartItemRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            return BaseResult<Guid>.Ok(existingCartItem.Guid);
        }
        else
        {
            var newCartItem = CartItem.Create(request.ProductId, request.Quantity);
            userCart.AddCartItem(newCartItem);
            await _cartItemRepository.UpdateAsync(userCart, cancellationToken);
            await _cartItemRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            return BaseResult<Guid>.Ok(newCartItem.Guid);
        }
    }

}
