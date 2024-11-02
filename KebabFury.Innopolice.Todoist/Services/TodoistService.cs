using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using KebabFury.Innopolice.Todoist.Dto.Requests;
using KebabFury.Innopolice.Todoist.Dto.Responses;

namespace KebabFury.Innopolice.Todoist.Services;

public sealed class TodoistService : ITodoistService
{
    private readonly HttpClient _httpClient = new HttpClient();

    public async Task<TaskCreateResponse> CreateTask(TaskCreateRequest request, string accessToken)
    {
        var message = new HttpRequestMessage(method: HttpMethod.Post, requestUri: "https://api.todoist.com/rest/v2/tasks");

        message.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

        message.Content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");

        using var response = await _httpClient.SendAsync(message);

        response.EnsureSuccessStatusCode();

        return JsonSerializer.Deserialize<TaskCreateResponse>(await response.Content.ReadAsStringAsync()) ??
               throw new InvalidOperationException();
    }
}
