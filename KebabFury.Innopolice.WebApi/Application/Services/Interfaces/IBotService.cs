using KebabFury.Innopolice.WebApi.Application.Dto.Bot;
using KebabFury.Innopolice.WebApi.Domain.Models;

namespace KebabFury.Innopolice.WebApi.Application.Services.Interfaces;

public interface IBotService : IBaseService<Bot>
{
    public Task<Bot> CreateAsync(Guid ownerId, BotCreateRequest botCreateRequest); // bot dto should be
}