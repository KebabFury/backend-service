using KebabFury.Innopolice.WebApi.Domain.Models;
using KebabFury.Innopolice.WebApi.Infrastructure.Context;

namespace KebabFury.Innopolice.WebApi.Infrastructure.Repositories;

public class BotRepository : BaseRepository<Bot>
{
    public BotRepository(DataContext context)
        : base(context, "bots") { }

    public void HelloWorld() { }
}

