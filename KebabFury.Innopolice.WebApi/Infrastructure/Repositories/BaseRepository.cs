using System.Linq.Expressions;
using KebabFury.Innopolice.WebApi.Domain.Common;
using KebabFury.Innopolice.WebApi.Infrastructure.Context;
using MongoDB.Driver;

namespace KebabFury.Innopolice.WebApi.Infrastructure.Repositories;

public class BaseRepository<TEntity> : IBaseRepository<TEntity>
    where TEntity : BaseModel
{
    protected readonly IMongoCollection<TEntity> _collection;

    protected BaseRepository(DataContext context, string collection)
    {
        _collection = context.GetCollection<TEntity>(collection);
    }

    public async Task AddEntityAsync(TEntity entity)
    {
        await _collection.InsertOneAsync(entity);
    }

    public async Task DeleteAsync(Guid id)
    {
        await _collection.DeleteOneAsync(e => e.Id == id);
    }

    public async Task<bool> DoesExist(Guid id)
    {
        var exists = await GetByIdAsync(id);
        return exists != null;
    }

    public async Task<List<TEntity>> GetAllAsync()
    {
        var allEntities = await _collection.FindAsync(Builders<TEntity>.Filter.Empty);
        return await allEntities.ToListAsync();
    }

    public async Task<TEntity?> GetByIdAsync(Guid id)
    {
        return await _collection.FindAsync(entity => entity.Id == id).Result.FirstOrDefaultAsync();
    }

    public async Task<List<TEntity>> SearchEntitiesAsync(Func<TEntity, bool> predicate)
    {
        Expression<Func<TEntity, bool>> expression = x => predicate(x);
        return await _collection.FindAsync(expression).Result.ToListAsync();
    }

    public async Task UpdateEntityAsync(Guid id, TEntity entity)
    {
        await _collection.ReplaceOneAsync(e => e.Id == id, entity);
    }
}
