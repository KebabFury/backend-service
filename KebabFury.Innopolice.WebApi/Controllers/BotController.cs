using KebabFury.Innopolice.WebApi.Application.Dto.Bot;
using KebabFury.Innopolice.WebApi.Application.Services.Interfaces;
using KebabFury.Innopolice.WebApi.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace KebabFury.Innopolice.WebApi.Controllers;

public class BotController : BaseController<Bot>
{
    private readonly IBotService _botService;
    
    public BotController(IBotService botService) : base(botService)
    {
        _botService = botService;
    }

    [HttpPost("create")]
    public async Task<Bot> Create([FromBody] BotCreateRequest botCreateRequest)
    {
        var createdBot = await _botService.CreateAsync(GetAccountId(), botCreateRequest);
        return createdBot;
    }
}