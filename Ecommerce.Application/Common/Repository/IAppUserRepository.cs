namespace Ecommerce.Application.Common.Repository;

public interface IAppUserRepository : IRepository<AppUser>
{
    Task<AppUser?> GetByGuidAsync(Guid customerGuid, CancellationToken cancellationToken);
    Task<AppUser?> GetByEmailAsync(string email, CancellationToken cancellationToken);
    Task<IEnumerable<AppUser>> GetAllAsync(CancellationToken cancellationToken);
    Task<AppUser> AddAsync(AppUser customer, CancellationToken cancellationToken);
    Task<AppUser> UpdateAsync(AppUser customer, CancellationToken cancellationToken);
    void Delete(AppUser customer, CancellationToken cancellationToken);
}

public interface IAppUserReadonlyRepository : IReadOnlyRepository<AppUser>
{
    Task<bool> ExistAsync(string email, CancellationToken cancellationToken);
}
