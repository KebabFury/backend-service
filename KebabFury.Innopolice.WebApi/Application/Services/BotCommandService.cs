using AutoMapper;
using KebabFury.Innopolice.WebApi.Application.Dto;
using KebabFury.Innopolice.WebApi.Application.Exceptions;
using KebabFury.Innopolice.WebApi.Application.Services.Interfaces;
using KebabFury.Innopolice.WebApi.Domain.Models;
using KebabFury.Innopolice.WebApi.Infrastructure.Repositories;

namespace KebabFury.Innopolice.WebApi.Application.Services;

public class BotCommandService : BaseService<BotCommand>, IBotCommandService
{
    private readonly BotCommandRepository _botCommandRepository;
    private readonly BotRepository _botRepository;
    private readonly IMapper _mapper;
    
    public BotCommandService(
        BotCommandRepository botCommandRepository,
        BotRepository botRepository,
        IMapper mapper) : base(botCommandRepository)
    {
        _botCommandRepository = botCommandRepository;
        _botRepository = botRepository;
        _mapper = mapper;
    }

    public async Task AddCommandsAsync(Guid botId, List<BotCommandDto> commands)
    {
        if (!await _botRepository.DoesExist(botId))
        {
            throw new EntityNotFoundException(botId, typeof(Bot));
        }
        foreach (var command in commands)
        {
            await AddCommandAsync(botId, command);
        }
    }

    private async Task AddCommandAsync(Guid botId, BotCommandDto command)
    {
        var commandModel = _mapper.Map<BotCommand>(command);
        commandModel.BotId = botId;

        await _botCommandRepository.AddEntityAsync(commandModel);
    }
}