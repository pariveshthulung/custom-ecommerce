namespace Ecommerce.Application.Features.CartFeature.Command;

public static class RemoveItemFromCartCommand
{
    #region Command
    public record Command(int CartItemId, bool Delete = false) : ICommand<BaseResult<Unit>>;
    #endregion
    #region Validation
    public class CommandValidator : AbstractValidator<Command>
    {
        public CommandValidator(ICartRepository cartRepository)
        {
            RuleFor(x => x.CartItemId).NotNull().NotEmpty().WithMessage("Invalid cart item.");

            RuleFor(x => x.CartItemId)
                .MustAsync(
                    async (id, cancellationToken) =>
                    {
                        var cart = await cartRepository.GetAllAsync(cancellationToken);
                        var cartItem = cart.SelectMany(x => x.CartItems)
                            .FirstOrDefault(x => x.Id == id);
                        return cartItem != null;
                    }
                )
                .WithMessage("Invalid cart item.");
        }
    }
    #endregion
    #region Handler
    public class Handler(ICartRepository cartRepository, ICurrentUserService currentUserService)
        : ICommandHandler<Command, BaseResult<Unit>>
    {
        public async Task<BaseResult<Unit>> Handle(
            Command request,
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
    #endregion
}
