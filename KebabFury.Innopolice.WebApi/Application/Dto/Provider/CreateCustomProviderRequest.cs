namespace KebabFury.Innopolice.WebApi.Application.Dto.Provider;

public record CreateCustomProviderRequest
{
    public required string Name { get; init; }
    public required string SwaggerJson { get; init; }
}