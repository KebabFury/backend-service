using KebabFury.Innopolice.WebApi.Domain.Types;

namespace KebabFury.Innopolice.WebApi.Application.Dto.Bot;

public record BotCommandDto
{
    public bool NeedAuth { get; init; }
    public string Description { get; init; }
    public string RequestUrl { get; init; }
    public RequestMethod RequestMethod { get; init; }
}