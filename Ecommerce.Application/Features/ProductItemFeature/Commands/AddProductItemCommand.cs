namespace Ecommerce.Application.Features.ProductItemFeature.Commands;

public static class AddProductItemCommand
{
    #region Command
    public record Command(Guid ProductGuid, string Image, string Sku, int Quantity, decimal Price)
        : ICommand<BaseResult<Guid>>;
    #endregion
    #region Handler
    public class Handler : ICommandHandler<Command, BaseResult<Guid>>
    {
        private readonly IProductRepository _productRepository;

        public Handler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<BaseResult<Guid>> Handle(
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
                request.Sku,
                request.Quantity,
                request.Price
            );
            product.AddProductItem(productItem);

            await _productRepository.UpdateAsync(product, cancellationToken);
            await _productRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
            return BaseResult<Guid>.Ok(productItem.Guid);
        }
    }
    #endregion
}
