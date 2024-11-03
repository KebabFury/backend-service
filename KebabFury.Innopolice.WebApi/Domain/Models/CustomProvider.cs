using KebabFury.Innopolice.WebApi.Domain.Common;
using KebabFury.Innopolice.WebApi.Domain.Types;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace KebabFury.Innopolice.WebApi.Domain.Models;

public class CustomProvider : BaseModel
{
    [BsonGuidRepresentation(GuidRepresentation.Standard)]
    public required Guid UserId { get; set; }
    public required string Name { get; set; }
    public required string Actions { get; set; }
    public required string Documentation { get; set; }
    public required string SwaggerJson { get; set; }
    
    public required string ClientId { get; set; }
    public required string ClientSecret { get; set; }
    public required string AuthorizationEndpoint { get; set; }
    public required string TokenEndpoint { get; set; }
    public required string RedirectUri { get; set; }
    public required string Scope { get; set; }
    
}