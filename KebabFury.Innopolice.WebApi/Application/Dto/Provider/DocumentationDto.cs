using System.Text.Json.Serialization;

namespace KebabFury.Innopolice.WebApi.Application.Dto.Provider;

public record DocumentationDto
{
    [JsonPropertyName("actions")]
    public required string Actions { get; init; }
    [JsonPropertyName("documentation")]
    public required string Documentation { get; init; }
}
