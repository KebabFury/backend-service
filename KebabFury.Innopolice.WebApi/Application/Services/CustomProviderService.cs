using System.Runtime.CompilerServices;
using System.Text.Json;
using KebabFury.Innopolice.WebApi.Application.Dto.Provider;
using KebabFury.Innopolice.WebApi.Application.Exceptions;
using KebabFury.Innopolice.WebApi.Application.Services.Interfaces;
using KebabFury.Innopolice.WebApi.Application.Settings;
using KebabFury.Innopolice.WebApi.Domain.Models;
using KebabFury.Innopolice.WebApi.Infrastructure.Repositories;
using Microsoft.Extensions.Options;
using HttpMethod = System.Net.Http.HttpMethod;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace KebabFury.Innopolice.WebApi.Application.Services;

public class CustomProviderService : BaseService<CustomProvider>, ICustomProviderService
{
    private readonly CustomProviderRepository _customProviderRepository;
    private readonly ParserSettings _parserSettings;
    private readonly HostSettings _hostSettings;
    private readonly DefaultUserAccount _defaultUserAccount;

    public CustomProviderService(
        IOptions<ParserSettings> parserSettings,
        IOptions<HostSettings> hostSettings,
        IOptions<DefaultUserAccount> defaultUserAccount,
        CustomProviderRepository customProviderRepository) : base(customProviderRepository)
    {
        _parserSettings = parserSettings.Value;
        _hostSettings = hostSettings.Value;
        _defaultUserAccount = defaultUserAccount.Value;
        _customProviderRepository = customProviderRepository;
    }

    public async Task<IList<CustomProvider>> ListByUserId(Guid userId) =>
        await _customProviderRepository.SearchEntitiesAsync(provider => provider.UserId == userId);

    public async Task CreateCustomProviderAsync(CreateCustomProviderRequest createRequest)
    {
        var providerName = await GetValidNameForProvider(createRequest.Name);
        var swaggerJson = createRequest.SwaggerJson;

        var documentationDto = await ParseSwaggerAsync(
            providerName, createRequest.ProviderDescription, createRequest.SwaggerJson);

        var callbackUrl = $"{_hostSettings.BaseUrl}/{createRequest.Name.ToLower()}/get-token";
        var provider = new CustomProvider
        {
            Id = Guid.NewGuid(),
            UserId = new Guid(_defaultUserAccount.Id),
            Name = providerName,
            ProviderDescription = createRequest.ProviderDescription,
            ActionCode = documentationDto.Actions,
            Documentation = documentationDto.Documentation,
            SwaggerJson = swaggerJson,
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

    public async Task DeepUpdateAsync(Guid id, CreateCustomProviderRequest request)
    {
        var provider = await _customProviderRepository.GetByIdAsync(id) ??
                       throw new EntityNotFoundException(id, typeof(CustomProvider));

        var providerName = provider.Name;
        if (provider.Name != request.Name)
        {
            providerName = await GetValidNameForProvider(request.Name);
        }

        var updatedDocumentationDto = await ParseSwaggerAsync(
            providerName, request.ProviderDescription, request.SwaggerJson);
        
        provider.Name = providerName;
        provider.ProviderDescription = request.ProviderDescription;
        provider.ActionCode = updatedDocumentationDto.Actions;
        provider.Documentation = updatedDocumentationDto.Documentation;
        provider.SwaggerJson = request.SwaggerJson;
        provider.ClientId = request.ClientId;
        provider.ClientSecret = request.ClientSecret;
        provider.AuthorizationEndpoint = request.AuthorizationEndpoint;
        provider.TokenEndpoint = request.TokenEndpoint;
        provider.Scope = request.Scope;

        await _customProviderRepository.UpdateEntityAsync(id, provider);
    }

    public async Task DeleteAll()
    {
        await _customProviderRepository.DeleteAllAsync();
    }

    private async Task<string> GetValidNameForProvider(string name)
    {
        var provider = await _customProviderRepository.GetByName(name);
        if (provider is null)
        {
            return name;
        }

        var index = 1;
        while (true)
        {
            var newName = $"{name}_{index}";
            var checkProvider = await _customProviderRepository.GetByName(newName);
            if (checkProvider is null)
            {
                return newName;
            }

            index++;
        }
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

    private async Task<DocumentationDto> ParseSwaggerAsync(string providerName, string description, string swaggerJson)
    {
        var httpClient = new HttpClient();
        var httpRequest = new HttpRequestMessage(HttpMethod.Post,
            $"{_parserSettings.ParseEndpointUrl}/body?provider={providerName}&description={description}");

        using var jsonDoc = JsonDocument.Parse(swaggerJson);
        var jsonContent = JsonContent.Create(jsonDoc.RootElement);
        httpRequest.Content = jsonContent;

        using var response = await httpClient.SendAsync(httpRequest);
        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync();
        var documentationDto = JsonSerializer.Deserialize<DocumentationDto>(responseContent);
        if (documentationDto?.Actions is null || documentationDto.Documentation is null)
        {
            throw new InvalidOperationException();
        }

        return documentationDto;
    }
}
