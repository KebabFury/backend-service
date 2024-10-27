using KebabFury.Innopolice.WebApi.Domain.Models;
using KebabFury.Innopolice.WebApi.Infrastructure.Context;
using MongoDB.Driver;

namespace KebabFury.Innopolice.WebApi.Infrastructure.Repositories;

public class UserRepository : BaseRepository<User>
{
    private readonly DataContext _context;

    public UserRepository(DataContext context)
        : base(context, "users")
    {
        _context = context;
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _collection.FindAsync(u => u.Email == email).Result.FirstOrDefaultAsync();
    }
}
