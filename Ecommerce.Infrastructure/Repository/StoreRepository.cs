namespace Ecommerce.Infrastructure.Repository;

public class StoreRepository(EcommerceDbContext context) : IStoreRepository
{
    public IUnitOfWork UnitOfWork => context;

    public async Task<Store> AddAsync(Store store, CancellationToken cancellationToken)
    {
        try
        {
            var entity = await context.Stores.AddAsync(store, cancellationToken);
            return entity.Entity;
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public void Delete(Store store)
    {
        try
        {
            context.Stores.Remove(store);
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public async Task<IEnumerable<Store>> GetAllAsync(CancellationToken cancellationToken)
    {
        try
        {
            return await context.Stores.ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public async Task<Store?> GetByGuidAsync(Guid storeGuid, CancellationToken cancellationToken)
    {
        try
        {
            return await context
                .Stores.AsTracking()
                .FirstOrDefaultAsync(
                    x => x.Guid == storeGuid && x.IsActive == true,
                    cancellationToken
                );
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public void Update(Store store)
    {
        try
        {
            context.Stores.Update(store);
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}

public class ReadonlyStoreRepository(EcommerceDbContext context) : IReadonlyStoreRepository
{
    public async Task<bool> Exist(Guid storeGuid, CancellationToken cancellationToken)
    {
        try
        {
            return await context.Stores.AnyAsync(x => x.Guid == storeGuid, cancellationToken);
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}
