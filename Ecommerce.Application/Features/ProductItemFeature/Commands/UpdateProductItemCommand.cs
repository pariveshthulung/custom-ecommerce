namespace Ecommerce.Application.Features.ProductItemFeature.Commands;

public static class UpdateProductItemCommand
{
    #region Command
    public record Command : ICommand<BaseResult<Response>>
    {
        public Guid ProductGuid { get; set; }
        public Guid ProductItemGuid { get; set; }
        public string Image { get; set; } = default!;
        public string SKU { get; set; } = default!;
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
    #endregion
    #region  Handler
    public class CommandHandler(IProductRepository productRepository)
        : ICommandHandler<Command, BaseResult<Response>>
    {
        public async Task<BaseResult<Response>> Handle(
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
                request.SKU,
                request.Quantity,
                request.Price
            );
            productRepository.Update(product);
            await productRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
            return BaseResult<Response>.Ok(new Response(productItem!.Guid));
        }
    }
    #endregion
    #region Response
    public record Response(Guid ProductItemGuid);
    #endregion
}
