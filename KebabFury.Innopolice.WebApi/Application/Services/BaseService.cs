using KebabFury.Innopolice.WebApi.Application.Exceptions;
using KebabFury.Innopolice.WebApi.Application.Services.Interfaces;
using KebabFury.Innopolice.WebApi.Domain.Common;
using KebabFury.Innopolice.WebApi.Infrastructure.Repositories;

namespace KebabFury.Innopolice.WebApi.Application.Services;

public abstract class BaseService<TEntity> : IBaseService<TEntity> where TEntity : BaseModel
{
    private IBaseRepository<TEntity> _repository;
    
    protected BaseService(
        IBaseRepository<TEntity> baseRepository)
    {
        _repository = baseRepository;
    }


    public async Task<TEntity> GetByIdAsync(Guid id)
    {
        return await _repository.GetByIdAsync(id) ?? throw new EntityNotFoundException(id, typeof(TEntity));
    }

    public async Task<IList<TEntity>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        await _repository.DeleteAsync(id);
    }

    public async Task UpdateAsync(TEntity entity)
    {
        if (!await _repository.DoesExist(entity.Id))
        {
            throw new EntityNotFoundException(entity.Id, typeof(TEntity));
        }

        await _repository.UpdateEntityAsync(entity.Id, entity);
    }
}