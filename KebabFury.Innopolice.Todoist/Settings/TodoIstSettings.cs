namespace KebabFury.Innopolice.TodoIst.Settings;

public record TodoIstSettings
{
    public string Oauth_Api_Url { get; init; }
    public string Token_Exchange_Api_Url { get; init; }
    public string RedirectUrl { get; init; }
    public string Client_id { get; init; }
    public string Scope { get; init; }
    public string State { get; init; }
    public string Client_Secret { get; init; }
}