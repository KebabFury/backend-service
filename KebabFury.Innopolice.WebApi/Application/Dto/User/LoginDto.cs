namespace KebabFury.Innopolice.WebApi.Application.Dto.User;

public record LoginDto
{
    public string Email { get; init; }
    public string Password { get; init; }
}