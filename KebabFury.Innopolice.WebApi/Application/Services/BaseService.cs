using KebabFury.Innopolice.WebApi.Infrastructure.Repositories;

namespace KebabFury.Innopolice.WebApi.Application.Services;

public abstract class BaseService<TEntity> : IBaseService<TEntity> where TEntity : class
{
    private IBaseRepository<TEntity> _repository;
    
    protected BaseService(IBaseRepository<TEntity> baseRepository)
    {
        _repository = baseRepository;
    }


    public async Task<TEntity> GetByIdAsync(Guid id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public Task<IList<TEntity>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<TEntity> UpdateAsync(TEntity entity)
    {
        throw new NotImplementedException();
    }
}