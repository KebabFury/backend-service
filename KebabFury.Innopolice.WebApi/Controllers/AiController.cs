using KebabFury.Innopolice.WebApi.Application.Dto.Bot;
using KebabFury.Innopolice.WebApi.Application.Services.Interfaces;
using KebabFury.Innopolice.WebApi.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace KebabFury.Innopolice.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class AiController : ControllerBase
{
    private readonly IAiService _aiService;

    public AiController(IAiService aiService)
    {
        _aiService = aiService;
    }

    [HttpPost("index")]
    public async Task Index([FromBody] BotCommand command)
    {
        await _aiService.IndexCommand(command);
    }

    [HttpPost("chat")]
    public async Task Chat([FromBody] QueryRequestDto queryRequest)
    {
        await _aiService.QueryAsync(queryRequest);
    }
    
    
}