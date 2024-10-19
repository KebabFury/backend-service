using KebabFury.Innopolice.WebApi.Domain.Types;

namespace KebabFury.Innopolice.WebApi.Application.Dto;

public class BotCommandDto
{
    public required bool NeedAuth { get; set; }
    public required string Description { get; set; }
    public required string RequestUrl { init; get; }
    public required RequestMethod RequestMethod { get; set; }
}