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

    public void Delete(AppUser customer)
    {
        try
        {
            ecommerceDbContext.Remove(customer);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error deleting user.");
            throw;
        }
    }

    public async Task<IEnumerable<AppUser>> GetAllAsync(CancellationToken cancellationToken)
    {
        try
        {
            return await ecommerceDbContext.AppUsers.ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error fetching list of user.");
            throw;
        }
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
            logger.LogError(ex, "Error fetching user.");
            throw;
        }
    }

    public async Task<AppUser?> GetByGuidAsync(Guid userGuid, CancellationToken cancellationToken)
    {
        try
        {
            return await ecommerceDbContext.AppUsers.FirstOrDefaultAsync(
                x => x.UserGuid == userGuid,
                cancellationToken
            );
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error fetching user.");
            throw;
        }
    }

    public void Update(AppUser user)
    {
        try
        {
            ecommerceDbContext.Update(user);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error updating user.");
            throw;
        }
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
