using KebabFury.Innopolice.WebApi.Domain.Models;
using KebabFury.Innopolice.WebApi.Infrastructure.Repositories;

namespace KebabFury.Innopolice.WebApi.Application.Services;

public class BotService : BaseService<Bot>, IBotService
{
    private readonly BotRepository _botRepository;
    
    public BotService(BotRepository botRepository) : base(botRepository)
    {
        _botRepository = botRepository;
    }

    public async Task<Bot> CreateAsync(Bot bot)
    {
        bot.Id = Guid.NewGuid();
        await _botRepository.AddEntityAsync(bot);
        return bot;
    }
}