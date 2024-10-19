using KebabFury.Innopolice.WebApi.Infrastructure.Context;
using KebabFury.Innopolice.WebApi.Infrastructure.Repositories;

namespace KebabFury.Innopolice.WebApi.Infrastructure;

public static class ConfigureData
{
    public static void ConfigureRepositories(this IServiceCollection services)
    {
        services.AddScoped<DataContext>();
        services.AddScoped<BotRepository>();
    }
}