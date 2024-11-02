namespace KebabFury.Innopolice.WebApi.Application.Dto.Provider;

public record CreateTaskRequest
{
    public required string Url { get; set; }
    public required string Method { get; set; }
    public string? Body { get; set; } = null;
    public Dictionary<string, string> Headers { get; set; } = new();
    public Dictionary<string, string> QueryParameters { get; set; } = new();
    public Dictionary<string, string> PathParameters { get; set; } = new();
}