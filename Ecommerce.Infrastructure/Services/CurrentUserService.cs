namespace Ecommerce.Infrastructure.Services;

public class CurrentUserService(IHttpContextAccessor httpContextAccessor) : ICurrentUserService
{
    public int UserId =>
        int.Parse(httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier)!);

    public string UserEmail =>
        httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Email)!;

    public bool IsAuthenticated =>
        httpContextAccessor.HttpContext?.User?.Identity?.IsAuthenticated ?? false;

    public string? StoreId => httpContextAccessor?.HttpContext?.User?.FindFirstValue("StoreId");

    public string UserRole =>
        httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Role)!;
}
