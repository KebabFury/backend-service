using KebabFury.Innopolice.WebApi.Application.Dto.Bot;
using KebabFury.Innopolice.WebApi.Domain.Models;

namespace KebabFury.Innopolice.WebApi.Application.Services.Interfaces;

public interface IAiService
{
    Task IndexCommand(BotCommand command);
    Task QueryAsync(QueryRequestDto queryRequest);
}