namespace Ecommerce.Application.Features.ProductFeature.Commands.Update;

public static class UpdateProductCommand
{
    #region Command
    public record Command : ICommand<BaseResult<Guid>>
    {
        public Guid ProductGuid { get; set; }
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
    }
    #endregion

    #region Validation
    public sealed class Validator : AbstractValidator<Command>
    {
        public Validator(IReadonlyProductRepository readonlyProductRepository)
        {
            RuleFor(x => x.ProductGuid).NotEmpty().WithMessage("Product id is required");
            RuleFor(x => x.ProductGuid)
                .MustAsync(readonlyProductRepository.ExistAsync)
                .WithMessage("Product doesnot exist");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required");
        }
    }
    #endregion

    #region Handler
    public class CommandHandler(
        IProductRepository productRepository,
        ICurrentUserService currentUserService
    ) : ICommandHandler<Command, BaseResult<Guid>>
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
            product.Update(
                request.Name,
                request.Description,
                currentUserService.UserEmail,
                currentUserService.UserId
            );

            productRepository.Update(product);
            await productRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            return BaseResult<Guid>.Ok(product.Guid);
        }
    }
    #endregion
}
