namespace KebabFury.Innopolice.WebApi.Application.Dto.User;

public record LoginDto
{
    public required string Email { get; init; }
    public required string Password { get; init; }
}
