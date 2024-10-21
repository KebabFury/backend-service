using AutoMapper;
using KebabFury.Innopolice.WebApi.Application.Dto.Bot;
using KebabFury.Innopolice.WebApi.Application.Services.Interfaces;
using KebabFury.Innopolice.WebApi.Domain.Models;
using KebabFury.Innopolice.WebApi.Infrastructure.Repositories;

namespace KebabFury.Innopolice.WebApi.Application.Services;

public class BotService : BaseService<Bot>, IBotService
{
    private readonly BotRepository _botRepository;
    private readonly IMapper _mapper;
    
    public BotService(
        BotRepository botRepository,
        IMapper mapper
        ) : base(botRepository)
    {
        _botRepository = botRepository;
        _mapper = mapper;
    }

    public async Task<Bot> CreateAsync(Guid ownerId, BotCreateRequest botCreateRequest)
    {
        var bot = _mapper.Map<Bot>(botCreateRequest);
        bot.Id = Guid.NewGuid();
        bot.OwnerId = ownerId;
        
        await _botRepository.AddEntityAsync(bot);
        return bot;
    }
}