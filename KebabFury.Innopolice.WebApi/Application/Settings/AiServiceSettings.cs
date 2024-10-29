namespace KebabFury.Innopolice.WebApi.Application.Settings;

public record AiServiceSettings
{
    public string BaseUrl { get; init; }
    public string IndexUrl { get; init; }
    public string ChatUrl { get; set; }
}