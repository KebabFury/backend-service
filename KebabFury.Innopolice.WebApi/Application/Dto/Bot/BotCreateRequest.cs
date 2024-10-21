namespace KebabFury.Innopolice.WebApi.Application.Dto.Bot;

public record BotCreateRequest
{
    public string TgToken { get; init; }
    public bool NeedAuth { get; init; }
    public string? OauthClient { get; init; }
    public string? OauthSecret { get; init; }
    public string? OauthHost { get; init; }
}