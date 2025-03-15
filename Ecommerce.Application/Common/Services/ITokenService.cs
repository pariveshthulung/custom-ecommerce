namespace Ecommerce.Application.Common.Services;

public interface ITokenService
{
    string GenerateAccessTokenAsync(AppUser user);

    string GenerateRefreshToken();

    bool IsTokenExpired(string token);

    // Task<string> GenerateNewAccessTokenAsync(string accessToken, string refreshToken);
    ClaimsPrincipal GetPrincipalFromExpiredToken(string Token);
}
