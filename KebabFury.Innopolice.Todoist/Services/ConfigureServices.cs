using KebabFury.Innopolice.Todoist.Services;
using Microsoft.Extensions.DependencyInjection;

namespace KebabFury.Innopolice.TodoIst.Services;

public static class ConfigureServices
{
    public static void ConfigureTodoIstServices(this IServiceCollection services)
    {
        services.AddScoped<ITodoIstAuthorizationService, TodoIstAuthorizationService>();
        services.AddScoped<ITodoistService, TodoistService>();
    }
}
