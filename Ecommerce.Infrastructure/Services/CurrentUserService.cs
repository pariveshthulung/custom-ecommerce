namespace Ecommerce.Infrastructure.Services;

public class CurrentUserService(
    IHttpContextAccessor httpContextAccessor,
    IServiceScopeFactory scopeFactory
) : ICurrentUserService
{
    public long UserId =>
        long.Parse(
            httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier)!
        );

    public string UserEmail =>
        httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Email)!;

    public bool IsAuthenticated =>
        httpContextAccessor.HttpContext?.User?.Identity?.IsAuthenticated ?? false;

    public string? StoreId => httpContextAccessor?.HttpContext?.User?.FindFirstValue("StoreGuid");

    public string UserRole =>
        httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Role)!;

    public async Task<AppUser?> GetCurrentUserAsync()
    {
        try
        {
            using var scope = scopeFactory.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<EcommerceDbContext>();

            if (UserEmail is null)
            {
                return null;
            }
            return await dbContext.AppUsers.FirstOrDefaultAsync(x => x.Email == UserEmail);
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}
