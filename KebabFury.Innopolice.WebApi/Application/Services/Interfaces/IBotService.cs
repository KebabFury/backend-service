using KebabFury.Innopolice.WebApi.Domain.Models;

namespace KebabFury.Innopolice.WebApi.Application.Services.Interfaces;

public interface IBotService : IBaseService<Bot>
{
    public Task<Bot> CreateAsync(Bot bot); // bot dto should be
}