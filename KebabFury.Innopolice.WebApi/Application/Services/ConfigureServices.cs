using KebabFury.Innopolice.WebApi.Application.Services.Interfaces;

namespace KebabFury.Innopolice.WebApi.Application.Services;

public static class ConfigureServices
{
    public static void ConfigureAllServices(this IServiceCollection services)
    {
        services.AddScoped<IBotService, BotService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IBotCommandService, BotCommandService>();
        services.AddScoped<IBotUserService, BotUserService>();
    }
}