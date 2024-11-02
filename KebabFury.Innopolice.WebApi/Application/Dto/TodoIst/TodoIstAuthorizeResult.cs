using System.Text.Json.Serialization;

namespace KebabFury.Innopolice.WebApi.Application.Dto.TodoIst;

public record TodoIstAuthorizeResult
{
    [JsonPropertyName("url")]
    public string Url { get; set; }

    public TodoIstAuthorizeResult(string url)
    {
        Url = url;
    }
}