using System.Text;
using System.Text.Json;
using KebabFury.Innopolice.Todoist.Dto.Requests;
using KebabFury.Innopolice.Todoist.Dto.Responses;
using KebabFury.Innopolice.TodoIst.Services;

namespace KebabFury.Innopolice.Todoist.Services;

public sealed class TodoistService : ITodoistService
{
    private readonly HttpClient _httpClient = new HttpClient();

    public async Task<TaskCreateResponse> CreateTask(TaskCreateRequest request)
    {
        var message = new HttpRequestMessage(method: HttpMethod.Post, requestUri: "https://api.todoist.com/rest/v2/tasks");

        // TODO need to add auth header 

        message.Content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");

        using var response = await _httpClient.SendAsync(message);
        response.EnsureSuccessStatusCode();
        return JsonSerializer.Deserialize<TaskCreateResponse>(await response.Content.ReadAsStringAsync());
    }
}
