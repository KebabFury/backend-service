using KebabFury.Innopolice.WebApi.Application.Dto;
using KebabFury.Innopolice.WebApi.Application.Dto.Bot;
using KebabFury.Innopolice.WebApi.Application.Services.Interfaces;
using KebabFury.Innopolice.WebApi.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace KebabFury.Innopolice.WebApi.Controllers;

public class BotCommandController : BaseController<BotCommand>
{
    private readonly IBotCommandService _botCommandService;
    
    public BotCommandController(IBotCommandService botCommandService) : base(botCommandService)
    {
        _botCommandService = botCommandService;
    }

    [HttpPost("{botId:guid}")]
    public async Task CreateCommand(Guid botId, [FromBody] BotCommandDto command)
    {
        await _botCommandService.AddCommandAsync(botId, command);
    }

    [HttpPost("{botId:guid}")]
    public async Task CreateCommands(Guid botId, [FromBody] List<BotCommandDto> commands)
    {
        await _botCommandService.AddCommandsAsync(botId, commands);
    }
}