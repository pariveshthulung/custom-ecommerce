namespace Ecommerce.Application.Features.ProductFeature.Commands;

public static class DeleteProductCommand
{
    #region Command
    public record Command(Guid ProductGuid) : ICommand<BaseResult<Unit>>;
    #endregion
    #region Validation
    public sealed class Validator : AbstractValidator<Command>
    {
        public Validator(IReadonlyProductRepository readonlyProductRepository)
        {
            RuleFor(x => x.ProductGuid).NotEmpty().WithMessage("Product id is required");
            RuleFor(x => x.ProductGuid)
                .MustAsync(readonlyProductRepository.ExistAsync)
                .WithMessage("Product doesnot exist");
        }
    }
    #endregion
    #region Handler
    public class Handler : ICommandHandler<Command, BaseResult<Unit>>
    {
        private readonly IProductRepository _productRepository;

        public Handler(IProductRepository productRepository)
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
            _productRepository.Delete(product);

            await _productRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
            return BaseResult<Unit>.Ok(Unit.Value);
        }
    }
    #endregion
}
