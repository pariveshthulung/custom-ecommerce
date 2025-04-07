namespace Ecommerce.Application.Features.ProductItemFeature.Commands;

public static class AddProductItemCommand
{
    #region Command
    public record Command : ICommand<BaseResult<Response>>
    {
        public Guid ProductGuid { get; set; }
        public string Image { get; set; } = default!;
        public string SKU { get; set; } = default!;
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
    #endregion
    #region  Validator
    public sealed class Validator : AbstractValidator<Command>
    {
        public Validator(IReadonlyProductRepository readonlyProductRepository)
        {
            RuleFor(x => x.Image).NotEmpty().WithMessage("Image is required");
            RuleFor(x => x.SKU).NotEmpty().WithMessage("Sku is required");
            RuleFor(x => x.Quantity).NotEmpty().GreaterThan(0).WithMessage("Invalid quantity");
            RuleFor(x => x.Price).NotEmpty().GreaterThan(0).WithMessage("Invalid price");
            RuleFor(x => x.ProductGuid)
                .MustAsync(readonlyProductRepository.ExistAsync)
                .WithMessage("Product does not exist");
        }
    }
    #endregion
    #region Handler
    public class Handler : ICommandHandler<Command, BaseResult<Response>>
    {
        private readonly IProductRepository _productRepository;

        public Handler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<BaseResult<Response>> Handle(
            Command request,
            CancellationToken cancellationToken
        )
        {
            var product = await _productRepository.GetByGuidAsync(
                request.ProductGuid,
                cancellationToken
            );
            var productItem = ProductItem.Create(
                request.Image,
                request.SKU,
                request.Quantity,
                request.Price
            );
            product.AddProductItem(productItem);

            _productRepository.Update(product);
            await _productRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
            return BaseResult<Response>.Ok(new Response(productItem.Guid));
        }
    }
    #endregion
    #region
    public record Response(Guid ProductItemGuid);
    #endregion
}
