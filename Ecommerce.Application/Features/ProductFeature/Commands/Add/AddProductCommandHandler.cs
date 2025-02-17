using Ecommerce.Application.Common.Repository;
using Ecommerce.Shared.Helper;

namespace Ecommerce.Application.Features.ProductFeature.Commands.Add;

public class AddProductCommandHandler
    : ICommandHandler<AddProductCommand, BaseResult<Guid>>
{
    private readonly IProductRepository _productRespository;
    private readonly ICurrentUserProvider _userProvider;

    public AddProductCommandHandler(IProductRepository productRepository,
        ICurrentUserProvider currentUserProvider)
    {
        _productRespository = productRepository;
        _userProvider = currentUserProvider;
    }
    public async Task<BaseResult<Guid>> Handle(AddProductCommand request, CancellationToken cancellationToken)
    {
        var newProduct = Product.Create(request.Name, request.Description);
        await _productRespository
            .AddAsync(newProduct, cancellationToken);
        await _productRespository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        return BaseResult<Guid>.Ok(newProduct.Guid);
    }
}
