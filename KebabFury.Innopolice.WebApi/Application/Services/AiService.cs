using System.Text;
using KebabFury.Innopolice.WebApi.Application.Dto.Bot;
using KebabFury.Innopolice.WebApi.Application.Services.Interfaces;
using KebabFury.Innopolice.WebApi.Application.Settings;
using KebabFury.Innopolice.WebApi.Domain.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace KebabFury.Innopolice.WebApi.Application.Services;

public class AiService : IAiService
{
    private readonly HttpClient _httpClient = new();
    private readonly AiServiceSettings _aiServiceSettings;
    
    public AiService(IOptions<AiServiceSettings> aiServiceSettings)
    {
        _aiServiceSettings = aiServiceSettings.Value;
    }
    
    public async Task IndexCommand(BotCommand command)
    {
        var request = CreatePostRequest(_aiServiceSettings.IndexUrl, command);

        using var response = await _httpClient.SendAsync(request);
        var responseString = await response.Content.ReadAsStringAsync();
        response.EnsureSuccessStatusCode();
        Console.WriteLine($"Response: {responseString}");
    }

    public async Task QueryAsync(QueryRequestDto queryRequest)
    {
        var request = CreatePostRequest(_aiServiceSettings.ChatUrl, queryRequest);

        using var response = await _httpClient.SendAsync(request);
        var responseString = await response.Content.ReadAsStringAsync();
        response.EnsureSuccessStatusCode();
        Console.WriteLine($"Response: {responseString}");
    }
    
    private HttpRequestMessage CreatePostRequest(string url, object data)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, GetFullRequestUrl(url));
        var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
        request.Content = content;
        return request;
    }

    private string GetFullRequestUrl(string url)
    {
        return $"{_aiServiceSettings.BaseUrl}/${url}";
    }
}