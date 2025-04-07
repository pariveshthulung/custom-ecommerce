using System.Data;

namespace Ecommerce.Application.Features.ProductFeature.Queries;

public static class GetAllProductQuery
{
    #region Query
    public record Query(long StoreId) : IQuery<BaseResult<Response>>;
    #endregion

    #region Validator
    public sealed class Validator : AbstractValidator<Query>
    {
        public Validator()
        {
            RuleFor(x => x.StoreId).NotEmpty().GreaterThan(0).WithMessage("Invalid store");
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
                var products = await productRepository.GetAllAsync(
                    request.StoreId,
                    cancellationToken
                );
                return BaseResult<Response>.Ok(
                    new Response(mapper.Map<IEnumerable<ProductModel>>(products))
                );
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error fetching products");
                throw;
            }
        }
    }
    #endregion

    #region Response
    public record Response(IEnumerable<ProductModel> ProductModels);
    #endregion
}
