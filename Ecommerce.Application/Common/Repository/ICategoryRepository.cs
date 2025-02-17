namespace Ecommerce.Application.Common.Repository;

public interface ICategoryRepository : IRepository<Category>
{
    Task<Category> GetByGuidAsync(Guid categoryGuid, CancellationToken cancellationToken = default(CancellationToken));
    Task<IEnumerable<Category>> GetAllAsync(CancellationToken cancellationToken = default(CancellationToken));
    Task<Category> AddAsync(Category category, CancellationToken cancellationToken = default(CancellationToken));
    Task<Category> UpdateAsync(Category category, CancellationToken cancellationToken = default(CancellationToken));
    void Delete(Category category, CancellationToken cancellationToken = default(CancellationToken));

}
