using System.Text;
using KebabFury.Innopolice.WebApi.Application.Dto.Provider;
using Microsoft.AspNetCore.Mvc;

namespace KebabFury.Innopolice.WebApi.Application.Services;

public static class ProviderEndpointHandleService
{
    
    public static async Task<JsonResult> HandleAsync(HandleEndpointRequest handleRequest)
    {
        var httpClient = new HttpClient();

        var fullUrl = handleRequest.Url;
        foreach (var param in handleRequest.Path)
        {
            fullUrl = fullUrl.Replace($"{{{param.Key}}}", param.Value);
        }
        
        var queryString = string.Join("&", handleRequest.Query.Select(q =>
            $"{q.Key}={Uri.EscapeDataString(q.Value)}"));
        fullUrl = string.IsNullOrEmpty(queryString) ? fullUrl : $"{fullUrl}?{queryString}";

        var httpRequest = new HttpRequestMessage
        {
            Method = new HttpMethod(handleRequest.Method),
            RequestUri = new Uri(fullUrl)
        };

        foreach (var header in handleRequest.Headers)
        {
            httpRequest.Headers.TryAddWithoutValidation(header.Key, header.Value);
        }

        // add body if method is "POST" or "PUT"
        if (handleRequest.Method.Equals("POST", StringComparison.OrdinalIgnoreCase) ||
            handleRequest.Method.Equals("PUT", StringComparison.OrdinalIgnoreCase))
        {
            if (handleRequest.Body is not null)
            {
                httpRequest.Content = new StringContent(handleRequest.Body, Encoding.UTF8, "application/json");
            }
        }

        if (handleRequest.BodyType != "application/json")
        {
            httpRequest.Headers.Add("Content-Type", handleRequest.BodyType);
        }

        try
        {
            var response = await httpClient.SendAsync(httpRequest);
            response.EnsureSuccessStatusCode();
            
            var responseContent = await response.Content.ReadAsStringAsync();
            return new JsonResult(new { response = responseContent, statusCode = StatusCodes.Status200OK });
        }
        catch (HttpRequestException httpEx) when (httpEx.StatusCode.HasValue)
        {
            var statusCode = (int)httpEx.StatusCode.Value;
            var statusMessage = $"HTTP request failed with status code {statusCode}";

            return new JsonResult(new
            {
                error = statusMessage,
                message = httpEx.Message,
                statusCode = statusCode
            });
        }
        catch (TaskCanceledException timeoutEx)
        {
            return new JsonResult(new 
            { 
                error = "Request timed out", 
                message = timeoutEx.Message, 
                statusCode = StatusCodes.Status408RequestTimeout 
            });
        }
        catch (Exception ex)
        {
            return new JsonResult(new 
            { 
                error = "An unexpected error occurred", 
                message = ex.Message, 
                statusCode = StatusCodes.Status500InternalServerError 
            });
        }
    }
}