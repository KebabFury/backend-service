using KebabFury.Innopolice.Todoist.Settings;

namespace KebabFury.Innopolice.WebApi.Application.Settings;

public static class ConfigureSettings
{
    public static void ConfigureAllSettings(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<DatabaseSettings>(configuration.GetSection("DatabaseSettings"));
        services.Configure<AiServiceSettings>(configuration.GetSection("AiServiceSettings"));
        services.Configure<TodoIstSettings>(configuration.GetSection("TodoIstSettings"));
    }
}
