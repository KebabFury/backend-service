using System.Text;
using System.Text.Json;
using KebabFury.Innopolice.Todoist.Settings;
using KebabFury.Innopolice.WebApi.Application.Dto.Provider;
using KebabFury.Innopolice.WebApi.Application.Exceptions;
using KebabFury.Innopolice.WebApi.Application.Services.Interfaces;
using KebabFury.Innopolice.WebApi.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;

namespace KebabFury.Innopolice.WebApi.Application.Services;

public class CustomProviderAuthorizationService : ICustomProviderAuthorizationService
{
    private readonly CustomProviderRepository _customProviderRepository;
    private readonly BaseHackathonSettings _baseHackathonSettings;

    public CustomProviderAuthorizationService(
        CustomProviderRepository customProviderRepository,
        IOptions<BaseHackathonSettings> baseHackathonSettings)
    {
        _customProviderRepository = customProviderRepository;
        _baseHackathonSettings = baseHackathonSettings.Value;
    }

    public async Task<AuthorizeResultDto> Authorize(string providerName)
    {
        var provider = await _customProviderRepository.GetByName(providerName);
        if (provider is null)
        {
            throw new ProviderNotFoundException(providerName);
        }
        
        var authorizationUrl = $"{provider.AuthorizationEndpoint}?" +
                               $"client_id={provider.ClientId}&" +
                               $"scope={provider.Scope}";
        return new AuthorizeResultDto(authorizationUrl);
    }

    public async Task<string> CallbackAsync(string providerName, string? code = null)
    {
        var httpClient = new HttpClient();
        var provider = await _customProviderRepository.GetByName(providerName);
        if (provider is null)
        {
            throw new ProviderNotFoundException(providerName);
        }
        
        var tokenParams = new Dictionary<string, string>
        {
            { "client_id", provider.ClientId },
            { "client_secret", provider.ClientSecret },
            { "code", code ?? "" },
            { "redirect_uri", provider.RedirectUri }
        };

        var response = await httpClient.PostAsync(
            provider.TokenEndpoint,
            new FormUrlEncodedContent(tokenParams));

        response.EnsureSuccessStatusCode();

        var responseData = await response.Content.ReadFromJsonAsync<JObject>();
        return responseData?.Value<string>("access_token") ?? throw new InvalidOperationException();
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
            throw;
        }
        catch (Exception ex)
        {
            return new JsonResult(new { detail = $"Error occurred while saving authorization data: {ex.Message}" }) { StatusCode = StatusCodes.Status500InternalServerError };
        }
    }
}