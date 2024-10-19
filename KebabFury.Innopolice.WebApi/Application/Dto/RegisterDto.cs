namespace KebabFury.Innopolice.WebApi.Application.Dto;

public record RegisterDto
{
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
}