using System.Text;
using System.Text.Json;
using KebabFury.Innopolice.Todoist.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace KebabFury.Innopolice.WebApi.Application.Services.Providers;

public class ProviderService : IProviderService
{
    private readonly BaseHackathonSettings _baseHackathonSettings;

    public ProviderService(IOptions<BaseHackathonSettings> baseHackthosSettings)
    {
        _baseHackathonSettings = baseHackthosSettings.Value;
    }
    public async Task<JsonResult> SaveAuthorizationDataAndReturnResponse(string authorizationData, string systemName)
    {
        var httpClient = new HttpClient();
        var header = new { Authorization = $"Bearer {_baseHackathonSettings.UserTokenFromTgBot}" };

        var data = new
        {
            system_name = systemName,
            authorization_data_json = JsonSerializer.Serialize(authorizationData)
        };

        var requestContent = new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json");
        requestContent.Headers.Add("Authorization", header.Authorization);

        try
        {
            var response = await httpClient.PostAsync(_baseHackathonSettings.SaveAuthDataEndpoint, requestContent);
            response.EnsureSuccessStatusCode();

            return response.IsSuccessStatusCode ? new JsonResult(new { message = "Authorization successful and data saved." }) { StatusCode = StatusCodes.Status200OK }
                : new JsonResult(new { message = $"Unexpected response: {response.StatusCode}" }) { StatusCode = (int)response.StatusCode };
        }
        catch (HttpRequestException ex) when (ex.StatusCode.HasValue)
        {
            return ex.StatusCode switch
            {
                System.Net.HttpStatusCode.BadRequest => new JsonResult(new { detail = "Bad request to save authorization data" }) { StatusCode = StatusCodes.Status400BadRequest },
                System.Net.HttpStatusCode.Unauthorized => new JsonResult(new { detail = "Unauthorized to save authorization data" }) { StatusCode = StatusCodes.Status401Unauthorized },
                System.Net.HttpStatusCode.Forbidden => new JsonResult(new { detail = "Forbidden to save authorization data" }) { StatusCode = StatusCodes.Status403Forbidden },
                System.Net.HttpStatusCode.NotFound => new JsonResult(new { detail = "Endpoint to save authorization data not found" }) { StatusCode = StatusCodes.Status404NotFound },
                System.Net.HttpStatusCode.InternalServerError or System.Net.HttpStatusCode.BadGateway => new JsonResult(new { detail = "Server error while saving authorization data" }) { StatusCode = StatusCodes.Status502BadGateway },
                _ => new JsonResult(new { detail = $"Unexpected error: {ex.Message}" }) { StatusCode = StatusCodes.Status500InternalServerError }
            };
        }
        catch (Exception ex)
        {
            return new JsonResult(new { detail = $"Error occurred while saving authorization data: {ex.Message}" }) { StatusCode = StatusCodes.Status500InternalServerError };
        }
    }
}
