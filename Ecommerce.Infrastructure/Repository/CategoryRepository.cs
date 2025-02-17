namespace Ecommerce.Infrastructure.Repository;

public class CategoryRepository : ICategoryRepository
{
    public IUnitOfWork UnitOfWork => throw new NotImplementedException();

    public Task<Category> AddAsync(Category category, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public void Delete(Category category, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Category>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Category> GetByGuidAsync(Guid categoryGuid, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Category> UpdateAsync(Category category, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
