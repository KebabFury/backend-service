namespace KebabFury.Innopolice.WebApi.Application.Dto;

public record LoginResultDto
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string AccessToken { get; set; }
}