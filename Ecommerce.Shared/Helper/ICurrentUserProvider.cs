using Ecommerce.Shared.Model;

namespace Ecommerce.Shared.Helper;

public interface ICurrentUserProvider
{
    Task<CurrentUser> GetCurrentUser();
}

public class CurrentUserProvider : ICurrentUserProvider
{
    public Task<CurrentUser> GetCurrentUser()
    {
        throw new NotImplementedException();
    }
}
