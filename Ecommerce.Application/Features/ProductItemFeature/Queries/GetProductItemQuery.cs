namespace Ecommerce.Application.Features.ProductItemFeature.Queries;

public static class GetProductItemQuery
{
    #region Query
    public record Query(Guid ProductGuid, Guid ProductItemGuid) : IQuery<BaseResult<Response>>;
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
            RuleFor(x => x)
                .MustAsync(
                    async (x, cancellationToken) =>
                    {
                        return await readonlyProductRepository.ExistAsync(
                            x.ProductGuid,
                            x.ProductItemGuid,
                            cancellationToken
                        );
                    }
                )
                .WithMessage("Product item does not exist");
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
                var productItem = product.ProductItems.FirstOrDefault(x =>
                    x.Guid == request.ProductItemGuid
                );
                return BaseResult<Response>.Ok(
                    new Response(mapper.Map<ProductItemModel>(productItem))
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
    public record Response(ProductItemModel ProductItemModel);

    #endregion
}
