namespace KebabFury.Innopolice.WebApi.Application.Dto.Bot;

public record QueryRequestDto
{
    public Guid UserId { get; init; }
    public Guid BotId { get; init; }
    public string Query { get; init; }
}