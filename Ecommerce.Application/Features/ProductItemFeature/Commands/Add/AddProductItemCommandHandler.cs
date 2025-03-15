using Ecommerce.Shared.Wrappers;

namespace Ecommerce.Application.Features.ProductItemFeature.Add;

public class AddProductItemCommandHandler : ICommandHandler<AddProductItemCommand, BaseResult<Guid>>
{
    private readonly IProductRepository _productRepository;

    public AddProductItemCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<BaseResult<Guid>> Handle(
        AddProductItemCommand request,
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
