using Ecommerce.Shared.Wrappers;

namespace Ecommerce.Application.Features.Carts.Command;

public record AddItemToCartCommand(int ProductId, int Quantity) : ICommand<BaseResult<Guid>>;

public class AddItemToCartCommandValidator : AbstractValidator<AddItemToCartCommand>
{
    public AddItemToCartCommandValidator(IProductRepository productRepository)
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
