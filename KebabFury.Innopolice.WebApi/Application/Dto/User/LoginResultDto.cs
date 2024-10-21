namespace KebabFury.Innopolice.WebApi.Application.Dto.User;

public record LoginResultDto
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    public string Email { get; init; }
    public string AccessToken { get; init; }
}