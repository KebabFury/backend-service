namespace KebabFury.Innopolice.WebApi.Application.Settings;

public record DefaultUserAccount
{
    public required string Id { get; init; }
    public required string Name { get; init; }
    public required string Email { get; init; }
    public required string Password { get; init; }
}