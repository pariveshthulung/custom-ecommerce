namespace Ecommerce.Application.Features.StoreFeature.Queries;

public static class GetStoreQuery
{
    #region Query
    public record Query(Guid StoreGuid) : IQuery<Response>;
    #endregion

    #region Validator
    public sealed class Validator : AbstractValidator<Query>
    {
        public Validator(IReadonlyStoreRepository readonlyStoreRepository)
        {
            RuleFor(x => x.StoreGuid)
                .NotEmpty()
                .WithMessage("Invalid Store")
                .MustAsync(readonlyStoreRepository.Exist)
                .WithMessage("Store does not exist.");
        }
    }
    #endregion

    #region Handler
    public class Handler(IStoreRepository storeRepository, IMapper mapper)
        : IQueryHandler<Query, Response>
    {
        public async Task<Response> Handle(Query request, CancellationToken cancellationToken)
        {
            try
            {
                var store = await storeRepository.GetByGuidAsync(
                    request.StoreGuid,
                    cancellationToken
                );
                return mapper.Map<Response>(store);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
    #endregion
    #region Response
    public record Response() : IMapFrom<Store>
    {
        public int MaxUser { get; set; }
        public string StoreName { get; set; } = default!;
    }
    #endregion
}
