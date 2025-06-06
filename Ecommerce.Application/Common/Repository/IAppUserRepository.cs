namespace Ecommerce.Application.Common.Repository;

public interface IAppUserRepository : IRepository<AppUser>
{
    Task<AppUser?> GetByGuidAsync(Guid customerGuid, CancellationToken cancellationToken);
    Task<AppUser?> GetByEmailAsync(string email, CancellationToken cancellationToken);
    Task<IEnumerable<AppUser>> GetAllAsync(CancellationToken cancellationToken);
    Task<AppUser> AddAsync(AppUser user, CancellationToken cancellationToken);
    void Update(AppUser user);
    void Delete(AppUser user);
}

public interface IAppUserReadonlyRepository : IReadOnlyRepository<AppUser>
{
    Task<bool> ExistAsync(string email, CancellationToken cancellationToken);
    Task<bool> StoreExistAsync(long appUserId, CancellationToken cancellationToken);
}
