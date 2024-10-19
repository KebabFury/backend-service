namespace KebabFury.Innopolice.WebApi.Application.Services.Interfaces;

public interface IBaseService<TEntity> where TEntity : class
{
    public Task<TEntity> GetByIdAsync(Guid id);
    public Task<IList<TEntity>> GetAllAsync();
    public Task DeleteAsync(Guid id);
    public Task UpdateAsync(TEntity entity);
}