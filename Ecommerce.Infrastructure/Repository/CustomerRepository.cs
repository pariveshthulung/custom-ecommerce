namespace Ecommerce.Infrastructure.Repository;

public class AppUserRepository(
    ILogger<AppUserRepository> logger,
    EcommerceDbContext ecommerceDbContext
) : IAppUserRepository
{
    public IUnitOfWork UnitOfWork => throw new NotImplementedException();

    public Task<AppUser> AddAsync(AppUser customer, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public void Delete(AppUser customer, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<AppUser>> GetAllAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<AppUser?> GetByEmailAsync(string email, CancellationToken cancellationToken)
    {
        try
        {
            return await ecommerceDbContext.AppUsers.FirstOrDefaultAsync(
                x => x.Email == email,
                cancellationToken
            );
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error updating user.");
            throw;
        }
    }

    public Task<AppUser?> GetByGuidAsync(Guid customerGuid, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<AppUser> UpdateAsync(AppUser customer, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}

public class AppUserReadonlyRepository(
    ILogger<AppUserReadonlyRepository> logger,
    EcommerceDbContext ecommerceDbContext
) : IAppUserReadonlyRepository
{
    public async Task<bool> ExistAsync(string email, CancellationToken cancellationToken)
    {
        try
        {
            return await ecommerceDbContext.AppUsers.AnyAsync(
                u => u.Email == email,
                cancellationToken
            );
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error checking user");
            throw;
        }
    }

    public async Task<bool> StoreExistAsync(long appUserId, CancellationToken cancellationToken)
    {
        try
        {
            return await ecommerceDbContext.AppUsers.AnyAsync(x => x.StoreId != null);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error checking store exist.");
            throw;
        }
    }
}
