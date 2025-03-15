using Ecommerce.Shared.Wrappers;

namespace Ecommerce.Application.Features.ProductFeature.Commands.Delete;

public class DeleteProductCommandHandler : ICommandHandler<DeleteProductCommand, BaseResult<Unit>>
{
    private readonly IProductRepository _productRepository;

    public DeleteProductCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<BaseResult<Unit>> Handle(
        DeleteProductCommand request,
        CancellationToken cancellationToken
    )
    {
        var product = await _productRepository.GetByGuidAsync(
            request.ProductGuid,
            cancellationToken
        );
        _productRepository.Delete(product);

        await _productRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        return BaseResult<Unit>.Ok(Unit.Value);
    }
}
