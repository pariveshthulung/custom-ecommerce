namespace Ecommerce.Application.Features.ProductFeature.Commands.Update;

public static class UpdateProductCommand
{
    #region Command
    public record Command(Guid ProductGuid, string Name, string Description)
        : ICommand<BaseResult<Guid>>;
    #endregion

    #region Validation
    public class Validation : AbstractValidator<Command>
    {
        public Validation(IProductRepository productRepository)
        {
            RuleFor(x => x.ProductGuid).NotNull().NotEmpty().WithMessage("Invalid product.");

            RuleFor(x => x.ProductGuid)
                .MustAsync(
                    async (guid, cancellationToken) =>
                    {
                        var product = await productRepository.GetByGuidAsync(
                            guid,
                            cancellationToken
                        );
                        return product is null ? false : true;
                    }
                )
                .WithMessage("Invalid product.");
        }
    }
    #endregion

    #region Handler
    public class CommandHandler(IProductRepository productRepository)
        : ICommandHandler<Command, BaseResult<Guid>>
    {
        public async Task<BaseResult<Guid>> Handle(
            Command request,
            CancellationToken cancellationToken
        )
        {
            var product = await productRepository.GetByGuidAsync(
                request.ProductGuid,
                cancellationToken
            );
            product.Update(request.Name, request.Description);

            await productRepository.UpdateAsync(product, cancellationToken);
            await productRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            return BaseResult<Guid>.Ok(product.Guid);
        }
    }
    #endregion
}
