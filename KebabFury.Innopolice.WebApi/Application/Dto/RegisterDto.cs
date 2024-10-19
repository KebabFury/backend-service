namespace KebabFury.Innopolice.WebApi.Application.Dto;

public record RegisterDto
{
    public required string Name { get; init; }
    public required string Email { get; init; }
    public required string Password { get; init; }
}