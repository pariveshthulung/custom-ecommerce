using System.Data;

namespace Ecommerce.Application.Features.UserFeature.Queries;

public static class GetUserQuery
{
    #region Query
    public record Query(Guid UserGuid) : IQuery<BaseResult<Response>>;
    #endregion
    #region Validator
    public sealed class Validator : AbstractValidator<Query>
    {
        public Validator()
        {
            RuleFor(x => x.UserGuid).NotEmpty().WithMessage("User id is required");
        }
    }
    #endregion
    #region Handler
    public sealed class Handler(
        ILogger<Handler> logger,
        IAppUserRepository appUserRepository,
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
                var user = await appUserRepository.GetByGuidAsync(
                    request.UserGuid,
                    cancellationToken
                );
                return BaseResult<Response>.Ok(new Response(mapper.Map<UserModel>(user)));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error fetching user");
                throw;
            }
        }
    }
    #endregion
    #region Response
    public record Response(UserModel UserModel);
    #endregion
}
