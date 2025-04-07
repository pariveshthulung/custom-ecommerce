namespace Ecommerce.Application.Features.ProductItemFeature.Queries;

public class GetProductItemListQuery
{
    #region Query
    public record Query(Guid ProductGuid) : IQuery<BaseResult<Response>>;
    #endregion
    #region Validator
    public sealed class Validator : AbstractValidator<Query>
    {
        public Validator(IReadonlyProductRepository readonlyProductRepository)
        {
            RuleFor(x => x.ProductGuid).NotEmpty().WithMessage("Product Guid is required");
            RuleFor(x => x.ProductGuid)
                .MustAsync(readonlyProductRepository.ExistAsync)
                .WithMessage("Product does not exist");
        }
    }
    #endregion
    #region Handler
    public sealed class Handler(
        ILogger<Handler> logger,
        IProductRepository productRepository,
        IMapper mapper
    ) : IQueryHandler<Query, BaseResult<Response>>
    {
        public async Task<BaseResult<Response>> Handle(
            Query request,
            CancellationToken cancellationToken
        )
        {
            try
            {
                var product = await productRepository.GetByGuidAsync(
                    request.ProductGuid,
                    cancellationToken
                );
                return BaseResult<Response>.Ok(
                    new Response(mapper.Map<IEnumerable<ProductItemModel>>(product.ProductItems))
                );
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error fetching product item");
                throw;
            }
        }
    }
    #endregion
    #region Response
    public record Response(IEnumerable<ProductItemModel> ProductItemModel);
    #endregion
}
