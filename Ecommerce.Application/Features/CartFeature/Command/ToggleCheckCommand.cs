namespace Ecommerce.Application.Features.CartFeature.Command;

public static class ToggleCheckCommand
{
    #region Command
    public record Command(Guid CartItemGuid, bool IsChecked) : ICommand<BaseResult<Guid>>;
    #endregion
    #region  Validation
    public sealed class CommandValidation : AbstractValidator<Command>
    {
        public CommandValidation(ICartRepository cartRepository, ICurrentUserService userProvider)
        {
            RuleFor(x => x.CartItemGuid).NotEmpty().NotNull().WithMessage("Invalid cart item.");

            RuleFor(x => x.CartItemGuid)
                .MustAsync(
                    async (guid, cancellationToken) =>
                    {
                        var cart = (
                            await cartRepository.GetAllAsync(cancellationToken)
                        ).FirstOrDefault(x => x.AppUserId == userProvider.UserId);
                        return cart!.CartItems.Any(x => x.Guid == guid);
                    }
                )
                .WithMessage("Invalid cart item.");
        }
    }
    #endregion
    #region  Handler
    public sealed class Handler(
        ICartRepository cartRepository,
        ICurrentUserService currentUserService,
        ILogger<Handler> logger
    ) : ICommandHandler<Command, BaseResult<Guid>>
    {
        public async Task<BaseResult<Guid>> Handle(
            Command request,
            CancellationToken cancellationToken
        )
        {
            try
            {
                var cart = (await cartRepository.GetAllAsync(cancellationToken)).FirstOrDefault(x =>
                    x.AppUserId == currentUserService.UserId
                );
                var item = cart?.CartItems.FirstOrDefault(x => x.Guid == request.CartItemGuid);
                item?.ModifyIsChecked(request.IsChecked);
                cartRepository.Update(cart!);
                await cartRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
                return BaseResult<Guid>.Ok(item!.Guid);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error toggling IsChecked property.");
                throw;
            }
        }
    }
    #endregion
}
