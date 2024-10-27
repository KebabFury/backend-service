namespace KebabFury.Innopolice.WebApi.Infrastructure.Repositories;

public interface IBaseRepository<TEntity>
    where TEntity : class
{
    Task<TEntity?> GetByIdAsync(Guid id);
    Task<List<TEntity>> SearchEntitiesAsync(Func<TEntity, bool> predicate);
    Task<List<TEntity>> GetAllAsync();
    Task<bool> DoesExist(Guid id);
    Task AddEntityAsync(TEntity entity);
    Task UpdateEntityAsync(Guid id, TEntity entity);
    Task DeleteAsync(Guid id);
}

