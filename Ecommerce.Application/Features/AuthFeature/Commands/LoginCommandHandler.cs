using Ecommerce.Shared.Wrappers;

namespace Ecommerce.Application.Features.AuthFeature.Commands;

public class LoginCommandHandler(
    ITokenService tokenService,
    // ILogger<LoginCommandHandler> logger,
    UserManager<AppUser> userManager,
    SignInManager<AppUser> signInManager
) : ICommandHandler<LoginCommand, BaseResult<LoginResponse>>
{
    public async Task<BaseResult<LoginResponse>> Handle(
        LoginCommand request,
        CancellationToken cancellationToken
    )
    {
        try
        {
            var user = await userManager.Users.FirstOrDefaultAsync(x => x.Email == request.Email);
            if (user is null)
                return BaseResult<LoginResponse>.Failure(
                    new Error(401, "Login", "Invalid username and password")
                );
            var signInResult = await signInManager.CheckPasswordSignInAsync(
                user,
                request.Password,
                false
            );

            if (!signInResult.Succeeded)
                return BaseResult<LoginResponse>.Failure(
                    new Error(401, "Login", "Invalid username and password")
                );
            if (user.IsPasswordExpire)
                return BaseResult<LoginResponse>.Failure(
                    new Error(403, "Login", "Password expire.Please update your password")
                );

            var accessToken = tokenService.GenerateAccessTokenAsync(user);
            var refershToken = tokenService.GenerateRefreshToken();
            return BaseResult<LoginResponse>.Ok(new LoginResponse(accessToken, refershToken));
        }
        catch (Exception ex)
        {
            // logger.LogError(ex, "Error authenticating user");
            throw;
        }
    }
}
