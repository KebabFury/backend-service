using System.Text.Json.Serialization;

namespace KebabFury.Innopolice.WebApi.Application.Dto.Provider;

public record HandleEndpointRequest
{
    [JsonPropertyName("url")] public required string Url { get; set; }
    [JsonPropertyName("method")] public required string Method { get; set; }
    [JsonPropertyName("body")] public string? Body { get; set; } = null;
    [JsonPropertyName("bodyType")] public string? BodyType { get; set; } = null;
    [JsonPropertyName("headers")] public Dictionary<string, string> Headers { get; init; } = new();
    [JsonPropertyName("query")] public Dictionary<string, string> Query { get; set; } = new();
    [JsonPropertyName("path")] public Dictionary<string, string> Path { get; set; } = new();
}