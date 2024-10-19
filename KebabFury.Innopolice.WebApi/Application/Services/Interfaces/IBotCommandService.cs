using KebabFury.Innopolice.WebApi.Application.Dto;
using KebabFury.Innopolice.WebApi.Domain.Models;

namespace KebabFury.Innopolice.WebApi.Application.Services.Interfaces;

public interface IBotCommandService : IBaseService<BotCommand>
{
    public Task AddCommandsAsync(Guid botId, List<BotCommandDto> commands);
}