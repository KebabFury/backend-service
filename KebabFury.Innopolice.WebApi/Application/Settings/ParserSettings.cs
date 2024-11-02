namespace KebabFury.Innopolice.WebApi.Application.Settings;

public record ParserSettings
{
    public required string ParseEndpointUrl { get; init; }
}