using KebabFury.Innopolice.Todoist.Settings;

namespace KebabFury.Innopolice.WebApi.Application.Settings;

public static class ConfigureSettings
{
    public static void ConfigureAllSettings(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<DatabaseSettings>(configuration.GetSection("DatabaseSettings"));
        services.Configure<TodoIstSettings>(configuration.GetSection("TodoIstSettings"));
        services.Configure<BaseHackathonSettings>(configuration.GetSection("BaseHackathonSettings"));
        services.Configure<ParserSettings>(configuration.GetSection("ParserSettings"));
        services.Configure<HostSettings>(configuration.GetSection("HostSettings"));
        services.Configure<DefaultUserAccount>(configuration.GetSection("DefaultAccount"));
    }
}
