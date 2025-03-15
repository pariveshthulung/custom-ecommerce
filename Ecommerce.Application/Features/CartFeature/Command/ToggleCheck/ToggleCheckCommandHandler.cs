using Ecommerce.Shared.Wrappers;

namespace Ecommerce.Application.Features.Carts.Command.ToggleCheck;

public class ToggleCheckCommandHandler(
    ICartRepository cartRepository,
    ICurrentUserService currentUserService,
    ILogger<ToggleCheckCommandHandler> logger
) : ICommandHandler<ToggleCheckCommand, BaseResult<Guid>>
{
    public async Task<BaseResult<Guid>> Handle(
        ToggleCheckCommand request,
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
            await cartRepository.UpdateAsync(cart!, cancellationToken);
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
