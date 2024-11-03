using KebabFury.Innopolice.WebApi.Domain.Models;
using KebabFury.Innopolice.WebApi.Infrastructure.Context;
using MongoDB.Driver;

namespace KebabFury.Innopolice.WebApi.Infrastructure.Repositories;

public class CustomProviderRepository : BaseRepository<CustomProvider>
{
    public CustomProviderRepository(DataContext context) : base(context, "providers")
    {
    }

    public async Task<CustomProvider?> GetByName(string name)
    {
        var filter = Builders<CustomProvider>.Filter.Eq(provider => provider.Name, name);
        var result = await _collection.Find(filter).FirstOrDefaultAsync();
        
        return result;
    }

    public async Task DeleteAllAsync()
    {
        var filter = Builders<CustomProvider>.Filter.Empty;
        await _collection.DeleteManyAsync(filter);
    }
}