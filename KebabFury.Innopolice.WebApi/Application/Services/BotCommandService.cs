using AutoMapper;
using KebabFury.Innopolice.WebApi.Application.Dto.Bot;
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
    private readonly ILogger<BotCommandService> _logger;

    public BotCommandService(
        BotCommandRepository botCommandRepository,
        BotRepository botRepository,
        IMapper mapper,
        ILogger<BotCommandService> logger
    )
        : base(botCommandRepository)
    {
        _botCommandRepository = botCommandRepository;
        _botRepository = botRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task AddCommandsAsync(Guid botId, List<BotCommandDto> commands)
    {
        try
        {
            if (!(await _botRepository.DoesExist(botId)))
            {
                throw new EntityNotFoundException(botId, typeof(Bot));
            }

            foreach (var command in commands)
            {
                await AddCommandAsync(botId, command);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            throw ex;
        }
    }

    public async Task AddCommandAsync(Guid botId, BotCommandDto command)
    {
        var commandModel = _mapper.Map<BotCommand>(command);
        commandModel.BotId = botId;

        await _botCommandRepository.AddEntityAsync(commandModel);
    }
}

