using Microsoft.AspNetCore.Http;

namespace Ecommerce.Application.Features.AuthFeature.Commands;

public static class LoginCommand
{
    #region Command
    public record Command : ICommand<BaseResult<Response>>
    {
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;
    }
    #endregion

    #region  Validator
    public class LoginCommandValidator : AbstractValidator<Command>
    {
        public LoginCommandValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Email is required.")
                .Matches(@"^[^@\s]+@[^@\s]+\.[^@\s]+$")
                .WithMessage("Invalid email format.");
        }
    }
    #endregion

    #region Handler
    public class LoginCommandHandler(
        ITokenService tokenService,
        // ILogger<LoginCommandHandler> logger,
        UserManager<AppUser> userManager,
        SignInManager<AppUser> signInManager
    ) : ICommandHandler<Command, BaseResult<Response>>
    {
        public async Task<BaseResult<Response>> Handle(
            Command request,
            CancellationToken cancellationToken
        )
        {
            try
            {
                var user = await userManager.Users.FirstOrDefaultAsync(x =>
                    x.Email == request.Email
                );
                if (user is null)
                    return BaseResult<Response>.Failure(
                        new Error(401, "Login", "Invalid username and password")
                    );
                var signInResult = await signInManager.CheckPasswordSignInAsync(
                    user,
                    request.Password,
                    false
                );

                if (!signInResult.Succeeded)
                    return BaseResult<Response>.Failure(
                        new Error(401, "Login", "Invalid username and password")
                    );
                if (user.IsPasswordExpire)
                    return BaseResult<Response>.Failure(
                        new Error(403, "Login", "Password expire.Please update your password")
                    );

                var accessToken = tokenService.GenerateAccessTokenAsync(user);
                var refershToken = tokenService.GenerateRefreshToken();
                return BaseResult<Response>.Ok(new Response(accessToken, refershToken));
            }
            catch (Exception ex)
            {
                // logger.LogError(ex, "Error authenticating user");
                throw;
            }
        }
    }
    #endregion

    #region Response
    public record Response(string accessToken, string refershToken)
    {
        public static object Cookies { get; internal set; }
    }
    #endregion
}
