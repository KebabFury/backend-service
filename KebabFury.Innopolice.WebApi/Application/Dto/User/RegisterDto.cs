namespace KebabFury.Innopolice.WebApi.Application.Dto.User;

public record RegisterDto
{
    public string Name { get; init; }
    public string Email { get; init; }
    public string Password { get; init; }
}