using System.Text.Json.Serialization;

namespace KebabFury.Innopolice.WebApi.Application.Dto.Provider;

public class AuthorizeResultDto
{
    [JsonPropertyName("url")]
    public string Url { get; set; }

    public AuthorizeResultDto(string url)
    {
        Url = url;
    }
}