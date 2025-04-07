namespace Ecommerce.Application.Features.ProductItemFeature.Commands;

public static class DeleteProductItemCommand
{
    public record Command(Guid ProductGuid, Guid ProductItemGuid) : ICommand<BaseResult<Unit>>;

    public class CommandHandler : ICommandHandler<Command, BaseResult<Unit>>
    {
        private readonly IProductRepository _productRepository;

        public CommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<BaseResult<Unit>> Handle(
            Command request,
            CancellationToken cancellationToken
        )
        {
            var product = await _productRepository.GetByGuidAsync(
                request.ProductGuid,
                cancellationToken
            );
            var productItem = product.ProductItems.FirstOrDefault(x =>
                x.Guid == request.ProductItemGuid
            );
            product.RemoveProductItem(productItem!);
            _productRepository.Update(product);
            await _productRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
            return BaseResult<Unit>.Ok(Unit.Value);
        }
    }
}
