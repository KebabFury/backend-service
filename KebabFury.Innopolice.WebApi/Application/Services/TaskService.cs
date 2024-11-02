using System.Text;
using KebabFury.Innopolice.WebApi.Application.Dto.Provider;
using KebabFury.Innopolice.WebApi.Application.Services.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace KebabFury.Innopolice.WebApi.Application.Services;

public static class TaskService
{
    
    public static async Task<JsonResult> CreateTask(CreateTaskRequest createRequest)
    {
        var httpClient = new HttpClient();

        var fullUrl = createRequest.PathParameters.Aggregate(createRequest.Url, (current, pathParam) =>
            current.Replace($"{{{pathParam.Key}}}", pathParam.Value));
        
        var queryString = string.Join("&", createRequest.QueryParameters.Select(q =>
            $"{q.Key}={Uri.EscapeDataString(q.Value)}"));
        fullUrl = string.IsNullOrEmpty(queryString) ? fullUrl : $"{fullUrl}?{queryString}";

        var httpRequest = new HttpRequestMessage
        {
            Method = new HttpMethod(createRequest.Method),
            RequestUri = new Uri(fullUrl)
        };

        foreach (var header in createRequest.Headers)
        {
            httpRequest.Headers.TryAddWithoutValidation(header.Key, header.Value);
        }

        // add body if method is "POST" or "PUT"
        if (createRequest.Method.Equals("POST", StringComparison.OrdinalIgnoreCase) ||
            createRequest.Method.Equals("PUT", StringComparison.OrdinalIgnoreCase))
        {
            if (createRequest.Body is not null)
            {
                httpRequest.Content = new StringContent(createRequest.Body, Encoding.UTF8, "application/json");
            }
        }

        var response = await httpClient.SendAsync(httpRequest);
        response.EnsureSuccessStatusCode();
        
        var responseContent = await response.Content.ReadAsStringAsync();
        return new JsonResult(new { response = responseContent, statusCode = StatusCodes.Status200OK });
    }
}