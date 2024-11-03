namespace KebabFury.Innopolice.WebApi.Application.Dto.Provider;

public record CustomProviderDocumentationDto
{
    public required string Name { get; set; }
    public required string ActionCode { get; set; }
    public required string Documentation { get; set; }
}