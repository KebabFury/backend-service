namespace KebabFury.Innopolice.WebApi.Application.Settings;

public record HostSettings
{
    public required string BaseUrl { get; set; }
}