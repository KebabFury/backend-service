namespace KebabFury.Innopolice.WebApi.Application.Services;

public static class ConfigureServices
{
    public static void ConfigureAllServices(this IServiceCollection services)
    {
        services.AddScoped<IBotService, BotService>();
    }
}