using KebabFury.Innopolice.WebApi.Domain.Types;

namespace KebabFury.Innopolice.WebApi.Application.Dto;

public record BotCommandDto
{
    public required bool NeedAuth { get; init; }
    public required string Description { get; init; }
    public required string RequestUrl { get; init; }
    public required RequestMethod RequestMethod { get; init; }
}