namespace Ecommerce.Application.Common.Services;

public interface ICurrentUserService
{
    long UserId { get; }
    string UserEmail { get; }
    string? StoreId { get; }
    string UserRole { get; }
    bool IsAuthenticated { get; }
}
