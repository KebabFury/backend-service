namespace KebabFury.Innopolice.WebApi.Domain.Models;

public sealed class Authorization
{
    public required string AccessToken { get; set; }
    public string? RefreshToken { get; set; }
    public long ExpiresAt { get; set; }

    public bool IsExpired
    {
        get
        {
            var dateTime = DateTimeOffset.FromUnixTimeMilliseconds(ExpiresAt);
            return dateTime < DateTimeOffset.Now;
        }
    }
}