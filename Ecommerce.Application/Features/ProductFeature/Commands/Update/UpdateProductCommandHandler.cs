using Ecommerce.Shared.Wrappers;

namespace Ecommerce.Application.Features.ProductFeature.Commands.Update;

public class UpdateProductCommandHandler(IProductRepository productRepository)
    : ICommandHandler<UpdateProductCommand, BaseResult<Guid>>
{
    public async Task<BaseResult<Guid>> Handle(
        UpdateProductCommand request,
        CancellationToken cancellationToken
    )
    {
        var product = await productRepository.GetByGuidAsync(
            request.ProductGuid,
            cancellationToken
        );
        product.Update(request.Name, request.Description);

        await productRepository.UpdateAsync(product, cancellationToken);
        await productRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        return BaseResult<Guid>.Ok(product.Guid);
    }
}
