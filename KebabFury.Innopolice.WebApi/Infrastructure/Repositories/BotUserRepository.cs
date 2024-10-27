using KebabFury.Innopolice.WebApi.Domain.Models;
using KebabFury.Innopolice.WebApi.Infrastructure.Context;

namespace KebabFury.Innopolice.WebApi.Infrastructure.Repositories;

public class BotUserRepository : BaseRepository<BotUser>
{
    public BotUserRepository(DataContext context)
        : base(context, "botUsers") { }
}

