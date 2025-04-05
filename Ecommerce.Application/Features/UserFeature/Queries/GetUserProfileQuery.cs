namespace Ecommerce.Application.Features.UserFeature.Queries;

public static class GetUserProfileQuery
{
    #region Query
    public record Query() : IQuery<BaseResult<Response>>;
    #endregion
    #region Validator
    public sealed class Validator : AbstractValidator<Query>
    {
        public Validator() { }
    }
    #endregion
    #region Handler
    public sealed class Handler(
        ILogger<Handler> logger,
        ICurrentUserService currentUserService,
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
                var user = await appUserRepository.GetByEmailAsync(
                    currentUserService.UserEmail,
                    cancellationToken
                );
                return BaseResult<Response>.Ok(new Response(mapper.Map<UserModel>(user)));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error getting user profile");
                throw;
            }
        }
    }
    #endregion
    #region Response
    public record Response(UserModel UserModel);
    #endregion
}
