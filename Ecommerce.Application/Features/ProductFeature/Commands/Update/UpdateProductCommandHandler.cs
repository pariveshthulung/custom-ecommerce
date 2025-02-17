using Ecommerce.Application.Common.Repository;
using Ecommerce.Shared.Helper;

namespace Ecommerce.Application.Features.ProductFeature.Commands.Update;

public class UpdateProductCommandHandler
    : ICommandHandler<UpdateProductCommand, BaseResult<Guid>>
{
    private readonly IProductRepository _productRepository;
    private readonly ICurrentUserProvider _currentUserProvider;

    public UpdateProductCommandHandler(IProductRepository productRepository,
         ICurrentUserProvider currentUserProvider)
    {
        _productRepository = productRepository;
        _currentUserProvider = currentUserProvider;
    }
    public async Task<BaseResult<Guid>> Handle(UpdateProductCommand request,
        CancellationToken cancellationToken)
    {
        var currentUser = await _currentUserProvider.GetCurrentUser();
        var product = await _productRepository
            .GetByGuidAsync(request.ProductGuid, cancellationToken);
        product.Update(request.Name, request.Description);

        await _productRepository.UpdateAsync(product, cancellationToken);
        await _productRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        return BaseResult<Guid>.Ok(product.Guid);
    }
}
