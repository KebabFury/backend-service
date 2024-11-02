namespace KebabFury.Innopolice.WebApi.Application.Dto.Provider;

public record CreateCustomProviderRequest
{
    public required string Name { get; init; }
    public required string SwaggerJson { get; init; }
    public required string ClientId { get; init; }
    public required string ClientSecret { get; init; }
    public required string AuthorizationEndpoint { get; init; }
    public required string TokenEndpoint { get; init; }
    public required string Scope { get; init; }

}