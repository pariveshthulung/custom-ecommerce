using Ecommerce.Shared.Wrappers;

namespace Ecommerce.Application.Features.ProductFeature.Commands.Update;

public record UpdateProductCommand(Guid ProductGuid, string Name, string Description)
    : ICommand<BaseResult<Guid>>;

public class UpdateProductCommandValidation : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidation(IProductRepository productRepository)
    {
        RuleFor(x => x.ProductGuid).NotNull().NotEmpty().WithMessage("Invalid product.");

        RuleFor(x => x.ProductGuid)
            .MustAsync(
                async (guid, cancellationToken) =>
                {
                    var product = await productRepository.GetByGuidAsync(guid, cancellationToken);
                    return product is null ? false : true;
                }
            )
            .WithMessage("Invalid product.");
    }
}
