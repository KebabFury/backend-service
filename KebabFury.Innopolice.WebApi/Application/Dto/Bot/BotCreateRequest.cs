namespace KebabFury.Innopolice.WebApi.Application.Dto.Bot;

public record BotCreateRequest
{
    public required string TgToken { get; init; }
    public required bool NeedAuth { get; init; }
    public required string? OauthClient { get; init; }
    public required string? OauthSecret { get; init; }
    public required string? OauthHost { get; init; }
}
