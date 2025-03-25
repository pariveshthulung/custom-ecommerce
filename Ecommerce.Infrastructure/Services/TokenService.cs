namespace Ecommerce.Infrastructure.Services;

public class TokenService(ILogger<TokenService> logger, IConfiguration configuration)
    : ITokenService
{
    public string GenerateAccessTokenAsync(AppUser user)
    {
        try
        {
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(configuration["JWT:SigningKey"] ?? "")
            );
            var credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
            var Claims = new List<Claim>
            {
                new(ClaimTypes.Email, user.Email ?? ""),
                new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new("StoreGuid", user.StoreGuid.ToString() ?? ""),
                new(ClaimTypes.Role, Enumeration.FromValue<RoleEnum>(user.RoleId).Name),
            };
            var tokenDiscriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(Claims),
                Issuer = configuration["JWT:Issuer"],
                Audience = configuration["JWT:Audience"],
                SigningCredentials = credential,
                Expires = DateTime.UtcNow.AddDays(1)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDiscriptor);
            return tokenHandler.WriteToken(token).ToString();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error generating token.");
            throw;
        }
    }

    public string GenerateRefreshToken()
    {
        try
        {
            var random = new byte[200];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(random);
            return Convert.ToBase64String(random);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error generating refresh token");
            throw;
        }
    }

    public ClaimsPrincipal GetPrincipalFromExpiredToken(string Token)
    {
        throw new NotImplementedException();
    }

    public bool IsTokenExpired(string token)
    {
        throw new NotImplementedException();
    }
}
