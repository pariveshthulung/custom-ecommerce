namespace Ecommerce.Application.Features.ProductFeature.Commands;

public static class AddProductCommand
{
    #region  Command
    public record Command(string Name, string Description) : ICommand<BaseResult<Guid>>;
    #endregion
    #region Validation
    public class AddProductCommandValidation : AbstractValidator<Command>
    {
        public AddProductCommandValidation()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Invalid name");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Invalid name");
        }
    }
    #endregion
    #region  Handler
    public class Handler(IProductRepository productRepository)
        : ICommandHandler<Command, BaseResult<Guid>>
    {
        public async Task<BaseResult<Guid>> Handle(
            Command request,
            CancellationToken cancellationToken
        )
        {
            var newProduct = Product.Create(request.Name, request.Description);
            await productRepository.AddAsync(newProduct, cancellationToken);
            await productRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
            return BaseResult<Guid>.Ok(newProduct.Guid);
        }
    }
    #endregion
}
