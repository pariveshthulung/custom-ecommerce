using System;
using Ecommerce.Application.Common.Repository;

namespace Ecommerce.Application.Features.ProductItemFeature.Update;

public class UpdateProductItemCommandHandler(IProductRepository productRepository)
    : ICommandHandler<UpdateProductItemCommand, BaseResult<Guid>>
{
    public async Task<BaseResult<Guid>> Handle(UpdateProductItemCommand request, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetByGuidAsync(request.ProductGuid, cancellationToken);
        var productItem = product.ProductItems.FirstOrDefault(x => x.Guid == request.ProductItemGuid);
        productItem?.UpdateProductItem(request.Image, request.Sku, request.Quantity, request.Price);
        await productRepository.UpdateAsync(product, cancellationToken);
        await productRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        return BaseResult<Guid>.Ok(productItem!.Guid);
    }
}
