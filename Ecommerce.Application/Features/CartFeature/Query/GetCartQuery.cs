namespace Ecommerce.Application.Features.CartFeature.Query;

public static class GetCartQuery
{
    #region  Query
    public record Query(Guid CartGuid) : IQuery<BaseResult<CartDto>>;
    #endregion

    #region Handler
    public class QueryHandler(
        ICartRepository cartRepository,
        ICurrentUserService currentUserService,
        IMapper mapper
    ) : IQueryHandler<Query, BaseResult<CartDto>>
    {
        public async Task<BaseResult<CartDto>> Handle(
            Query request,
            CancellationToken cancellationToken
        )
        {
            var cart = await cartRepository.GetByUserIdAsync(
                currentUserService.UserId,
                cancellationToken
            );
            return BaseResult<CartDto>.Ok(mapper.Map<CartDto>(cart));
        }
    }
    #endregion
}
