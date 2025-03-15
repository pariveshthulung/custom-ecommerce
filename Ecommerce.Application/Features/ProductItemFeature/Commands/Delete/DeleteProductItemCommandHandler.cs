using Ecommerce.Shared.Wrappers;

namespace Ecommerce.Application.Features.ProductItemFeature.Delete;

public class DeleteProductItemCommandHandler
    : ICommandHandler<DeleteProductItemCommand, BaseResult<Unit>>
{
    private readonly IProductRepository _productRepository;

    public DeleteProductItemCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<BaseResult<Unit>> Handle(
        DeleteProductItemCommand request,
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
        await _productRepository.UpdateAsync(product, cancellationToken);
        await _productRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        return BaseResult<Unit>.Ok(Unit.Value);
    }
}
