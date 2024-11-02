using System.Net;
using System.Text;
using KebabFury.Innopolice.Todoist.Settings;
using KebabFury.Innopolice.WebApi.Application.Dto.Provider;
using KebabFury.Innopolice.WebApi.Application.Exceptions;
using KebabFury.Innopolice.WebApi.Application.Services.Interfaces;
using KebabFury.Innopolice.WebApi.Application.Settings;
using KebabFury.Innopolice.WebApi.Domain.Models;
using KebabFury.Innopolice.WebApi.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace KebabFury.Innopolice.WebApi.Application.Services;

public class ProviderService : BaseService<CustomProvider>, IProviderService
{
    private readonly CustomProviderRepository _customProviderRepository;
    private readonly BaseHackathonSettings _baseHackathonSettings;
    private readonly ParserSettings _parserSettings;
    private readonly HostSettings _hostSettings;

    public ProviderService(
        IOptions<BaseHackathonSettings> baseHackathonSettings,
        IOptions<ParserSettings> parserSettings,
        IOptions<HostSettings> hostSettings,
        CustomProviderRepository customProviderRepository) : base(customProviderRepository)
    {
        _baseHackathonSettings = baseHackathonSettings.Value;
        _parserSettings = parserSettings.Value;
        _hostSettings = hostSettings.Value;
        _customProviderRepository = customProviderRepository;
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
                HttpStatusCode.BadRequest => new JsonResult(new { detail = "Bad request to save authorization data" }) { StatusCode = StatusCodes.Status400BadRequest },
                HttpStatusCode.Unauthorized => new JsonResult(new { detail = "Unauthorized to save authorization data" }) { StatusCode = StatusCodes.Status401Unauthorized },
                HttpStatusCode.Forbidden => new JsonResult(new { detail = "Forbidden to save authorization data" }) { StatusCode = StatusCodes.Status403Forbidden },
                HttpStatusCode.NotFound => new JsonResult(new { detail = "Endpoint to save authorization data not found" }) { StatusCode = StatusCodes.Status404NotFound },
                HttpStatusCode.InternalServerError or HttpStatusCode.BadGateway => new JsonResult(new { detail = "Server error while saving authorization data" }) { StatusCode = StatusCodes.Status502BadGateway },
                _ => new JsonResult(new { detail = $"Unexpected error: {ex.Message}" }) { StatusCode = StatusCodes.Status500InternalServerError }
            };
        }
        catch (Exception ex)
        {
            return new JsonResult(new { detail = $"Error occurred while saving authorization data: {ex.Message}" }) { StatusCode = StatusCodes.Status500InternalServerError };
        }
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

    public async Task<string> CallbackAsync(string providerName, string? code = null, string? state = null)
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
            provider.AuthorizationEndpoint,
            new FormUrlEncodedContent(tokenParams));

        response.EnsureSuccessStatusCode();

        var responseData = await response.Content.ReadFromJsonAsync<JObject>();
        return responseData?.Value<string>("access_token") ?? throw new InvalidOperationException();
    }

    public async Task CreateCustomAsync(CreateCustomProviderRequest createRequest)
    {
        var httpClient = new HttpClient();
        var httpRequest = new HttpRequestMessage(HttpMethod.Post, _parserSettings.ParseEndpointUrl);
        httpRequest.Content = new StringContent(createRequest.SwaggerJson, Encoding.UTF8, "application/json");

        using var response = await httpClient.SendAsync(httpRequest);
        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync();
        var documentationDto = JsonSerializer.Deserialize<DocumentationDto>(responseContent);
        if (documentationDto?.ActionCode is null || documentationDto?.Documentation is null)
        {
            throw new InvalidOperationException();
        }

        var callbackUrl = $"{_hostSettings.BaseUrl}/{createRequest.Name.ToLower()}/get-token";
        var provider = new CustomProvider
        {
            Id = Guid.NewGuid(),
            Name = createRequest.Name,
            ActionCode = documentationDto.ActionCode,
            Documentation = documentationDto.Documentation,
            ClientId = createRequest.ClientId,
            ClientSecret = createRequest.ClientSecret,
            AuthorizationEndpoint = createRequest.AuthorizationEndpoint,
            TokenEndpoint = createRequest.TokenEndpoint,
            RedirectUri = callbackUrl,
            Scope = createRequest.Scope
        };
        await _customProviderRepository.AddEntityAsync(provider);
    }

    public async Task<IList<CustomProviderDocumentationDto>> ListAllDocumentations()
    {
        var customProviders = await _customProviderRepository.GetAllAsync();
        var documentations = customProviders.Select(GetDocumentationDto).ToList();
        return documentations;
    }

    private CustomProviderDocumentationDto GetDocumentationDto(CustomProvider provider)
    {
        return new CustomProviderDocumentationDto
        {
            Name = provider.Name,
            ActionCode = provider.ActionCode,
            Documentation = provider.Documentation
        };
    }
}
