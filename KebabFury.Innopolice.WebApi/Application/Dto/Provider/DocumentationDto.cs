namespace KebabFury.Innopolice.WebApi.Application.Dto.Provider;

public record DocumentationDto
{
    public required string ActionCode { get; init; }
    public required string Documentation { get; init; }
}