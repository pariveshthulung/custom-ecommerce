namespace Ecommerce.Application.Features.ProductItemFeature.Commands;

public static class UpdateProductItemCommand
{
    #region Command
    public record Command(
        Guid ProductGuid,
        Guid ProductItemGuid,
        string Image,
        string Sku,
        int Quantity,
        decimal Price
    ) : ICommand<BaseResult<Guid>>;
    #endregion
    #region  Handler
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
            var productItem = product.ProductItems.FirstOrDefault(x =>
                x.Guid == request.ProductItemGuid
            );
            productItem?.UpdateProductItem(
                request.Image,
                request.Sku,
                request.Quantity,
                request.Price
            );
            await productRepository.UpdateAsync(product, cancellationToken);
            await productRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
            return BaseResult<Guid>.Ok(productItem!.Guid);
        }
    }
    #endregion
}
