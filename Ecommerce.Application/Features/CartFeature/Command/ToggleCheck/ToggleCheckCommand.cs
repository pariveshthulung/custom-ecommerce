using Ecommerce.Shared.Wrappers;

namespace Ecommerce.Application.Features.Carts.Command.ToggleCheck;

public record ToggleCheckCommand(Guid CartItemGuid, bool IsChecked) : ICommand<BaseResult<Guid>>;

public class CommandValidation : AbstractValidator<ToggleCheckCommand>
{
    public CommandValidation(ICartRepository cartRepository, ICurrentUserService userProvider)
    {
        RuleFor(x => x.CartItemGuid).NotEmpty().NotNull().WithMessage("Invalid cart item.");

        RuleFor(x => x.CartItemGuid)
            .MustAsync(
                async (guid, cancellationToken) =>
                {
                    var cart = (await cartRepository.GetAllAsync(cancellationToken)).FirstOrDefault(
                        x => x.AppUserId == userProvider.UserId
                    );
                    return cart!.CartItems.Any(x => x.Guid == guid);
                }
            )
            .WithMessage("Invalid cart item.");
    }
}
