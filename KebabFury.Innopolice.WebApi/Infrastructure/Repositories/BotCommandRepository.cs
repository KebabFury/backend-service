using KebabFury.Innopolice.WebApi.Domain.Models;
using KebabFury.Innopolice.WebApi.Infrastructure.Context;

namespace KebabFury.Innopolice.WebApi.Infrastructure.Repositories;

public class BotCommandRepository : BaseRepository<BotCommand>
{
    public BotCommandRepository(DataContext context)
        : base(context, "botCommands") { }
}
