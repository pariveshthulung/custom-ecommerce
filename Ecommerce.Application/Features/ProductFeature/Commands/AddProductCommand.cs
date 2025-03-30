namespace Ecommerce.Application.Features.ProductFeature.Commands;

public static class AddProductCommand
{
    #region  Command
    public record Command : ICommand<BaseResult<Guid>>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
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
    public class Handler(
        IProductRepository productRepository,
        IStoreRepository storeRepository,
        ICurrentUserService currentUserService
    ) : ICommandHandler<Command, BaseResult<Guid>>
    {
        public async Task<BaseResult<Guid>> Handle(
            Command request,
            CancellationToken cancellationToken
        )
        {
            var storeGuid = Guid.Parse(currentUserService.StoreId);
            var newProduct = Product.Create(request.Name, request.Description);
            var product = await productRepository.AddAsync(newProduct, cancellationToken);
            var store = await storeRepository.GetByGuidAsync(storeGuid, cancellationToken);
            store.AddProduct(product.Id);
            storeRepository.Update(store);
            await productRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
            return BaseResult<Guid>.Ok(newProduct.Guid);
        }
    }
    #endregion
}
