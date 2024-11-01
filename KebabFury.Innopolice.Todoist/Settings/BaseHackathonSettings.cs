namespace KebabFury.Innopolice.TodoIst.Settings;

public record BaseHackathonSettings
{
    public string UserTokenFromTgBot { get; init; }
    public string SaveAuthDataEndpoint { get; init; }
}