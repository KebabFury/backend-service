using KebabFury.Innopolice.WebApi.Domain.Common;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace KebabFury.Innopolice.WebApi.Domain.Models;

public sealed class Bot : BaseModel
{
    [BsonGuidRepresentation(GuidRepresentation.Standard)]
    public Guid OwnerId { get; set; }
    public string? TgToken { get; set; }

    public bool NeedAuth { get; set; }
    public string? OauthClient { get; set; }
    public string? OauthSecret { get; set; }
    public string? OauthHost { get; set; }
}