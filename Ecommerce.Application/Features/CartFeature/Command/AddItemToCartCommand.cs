namespace Ecommerce.Application.Features.CartFeature.Command;

public static class AddItemToCartCommand
{
    #region Command
    public record Command(int ProductId, int Quantity) : ICommand<BaseResult<Guid>>;
    #endregion

    #region  Validation
    public class CommandValidator : AbstractValidator<Command>
    {
        public CommandValidator(IReadonlyProductRepository productRepository)
        {
            RuleFor(x => x.ProductId).NotNull().NotEmpty().WithMessage("Invalid Product");

            RuleFor(x => x.ProductId)
                .MustAsync(
                    async (id, CancellationToken) =>
                    {
                        return await productRepository.ExistAsync(id, CancellationToken);
                    }
                )
                .WithMessage("Invalid Product");

            RuleFor(x => x.Quantity).NotNull().NotEmpty().WithMessage("Invalid Quantity");
        }
    }
    #endregion

    #region Handler
    public class Handler(ICartRepository cartRepository, ICurrentUserService currentUserService)
        : ICommandHandler<Command, BaseResult<Guid>>
    {
        public async Task<BaseResult<Guid>> Handle(
            Command request,
            CancellationToken cancellationToken
        )
        {
            var userCart = await cartRepository.GetByUserIdAsync(
                currentUserService.UserId,
                cancellationToken
            );

            return await AddOrUpdateCartItemAsync(userCart, request, cancellationToken);
        }

        private async Task<BaseResult<Guid>> AddOrUpdateCartItemAsync(
            Cart userCart,
            Command request,
            CancellationToken cancellationToken
        )
        {
            var existingCartItem = userCart.CartItems.SingleOrDefault(item =>
                item.ProductId == request.ProductId
            );

            if (existingCartItem != null)
            {
                existingCartItem.AddQuantity(request.Quantity);
                cartRepository.Update(userCart);
                await cartRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

                return BaseResult<Guid>.Ok(existingCartItem.Guid);
            }
            else
            {
                var newCartItem = CartItem.Create(request.ProductId, request.Quantity);
                userCart.AddCartItem(newCartItem);
                cartRepository.Update(userCart);
                await cartRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

                return BaseResult<Guid>.Ok(newCartItem.Guid);
            }
        }
    }
    #endregion
}
