using KebabFury.Innopolice.WebApi.Application.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace KebabFury.Innopolice.WebApi.Infrastructure.Context;

public class DataContext
{
    private readonly IMongoDatabase _database;

    public DataContext(IOptions<DatabaseSettings> databaseSettings)
    {
        var client = new MongoClient(databaseSettings.Value.ConnectionString);
        _database = client.GetDatabase(databaseSettings.Value.DatabaseName);
    }

    public IMongoCollection<T> GetCollection<T>(string collectionName)
    {
        return _database.GetCollection<T>(collectionName);
    }
}
