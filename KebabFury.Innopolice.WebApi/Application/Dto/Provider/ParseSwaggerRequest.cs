namespace KebabFury.Innopolice.WebApi.Application.Dto.Provider;

public record ParseSwaggerRequest
{
    public required string JsonData { get; set; }
}