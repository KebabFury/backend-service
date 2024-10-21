namespace KebabFury.Innopolice.WebApi.Application.Dto.User;

public record LoginResultDto
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public required string Email { get; init; }
    public required string AccessToken { get; init; }
}
