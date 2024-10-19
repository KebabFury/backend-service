using KebabFury.Innopolice.WebApi.Application.Services.Interfaces;
using KebabFury.Innopolice.WebApi.Domain.Models;
using KebabFury.Innopolice.WebApi.Infrastructure.Repositories;

namespace KebabFury.Innopolice.WebApi.Application.Services;

public class BotUserService : BaseService<BotUser>, IBotUserService
{
    private readonly BotUserRepository _botUserRepository;
    
    public BotUserService(BotUserRepository botUserRepository) : base(botUserRepository)
    {
        _botUserRepository = botUserRepository;
    }
}