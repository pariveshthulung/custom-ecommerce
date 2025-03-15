using Ecommerce.Shared.Wrappers;

namespace Ecommerce.Application.Features.ProductFeature.Commands.Add;

public class AddProductCommandHandler(IProductRepository productRepository)
    : ICommandHandler<AddProductCommand, BaseResult<Guid>>
{
    public async Task<BaseResult<Guid>> Handle(
        AddProductCommand request,
        CancellationToken cancellationToken
    )
    {
        var newProduct = Product.Create(request.Name, request.Description);
        await productRepository.AddAsync(newProduct, cancellationToken);
        await productRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        return BaseResult<Guid>.Ok(newProduct.Guid);
    }
}
