using System.Net.Http.Json;
using KebabFury.Innopolice.TodoIst.Settings;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;

namespace KebabFury.Innopolice.TodoIst.Services;

public class TodoIstAuthorizationService : ITodoIstAuthorizationService
{
    private readonly TodoIstSettings _todoIstSettings;
    
    public TodoIstAuthorizationService(IOptions<TodoIstSettings> todoIstSettings)
    {
        _todoIstSettings = todoIstSettings.Value;
    }
    
    public string Authorize()
    {
        var authorizationUrl = $"{_todoIstSettings.Oauth_Api_Url}?" +
                               $"client_id={_todoIstSettings.Client_id}&" +
                               $"scope={_todoIstSettings.Scope}&" +
                               $"state={_todoIstSettings.State}";

        return authorizationUrl;
    }

    public async Task<string> CallbackAsync(string code = null, string state = null, string error = null)
    {
        var httpClient = new HttpClient();
        if (state != _todoIstSettings.State)
        {
            throw new Exception("State parameter mismatch");
        }

        // Prepare token exchange parameters
        var tokenParams = new Dictionary<string, string>
        {
            { "client_id", _todoIstSettings.Client_id },
            { "client_secret", _todoIstSettings.Client_Secret },
            { "code", code },
            { "redirect_uri", _todoIstSettings.RedirectUrl }
        };

        try
        {
            var response = await httpClient.PostAsync(
                _todoIstSettings.Token_Exchange_Api_Url, 
                new FormUrlEncodedContent(tokenParams)
            );

            response.EnsureSuccessStatusCode();

            var responseData = await response.Content.ReadFromJsonAsync<JObject>();
            return responseData?["access_token"]?.ToString();
        }
        catch (HttpRequestException ex)
        {
            var errorType = ex.Data["error"].ToString();
            throw errorType switch
            {
                "bad_authorization_code" => new Exception("Bad authorization code"),
                "incorrect_application_credentials" => new UnauthorizedAccessException(
                    "Incorrect application credentials"),
                _ => new Exception("Token exchange failed")
            };
        }
        catch (Exception ex)
        {
            throw new Exception($"Failed to communicate with Todoist: {ex.Message}");
        }
    }
}