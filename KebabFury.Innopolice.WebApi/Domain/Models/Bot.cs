using KebabFury.Innopolice.WebApi.Domain.Common;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace KebabFury.Innopolice.WebApi.Domain.Models;

public sealed class Bot : BaseModel
{
    [BsonGuidRepresentation(GuidRepresentation.Standard)]
    public required Guid OwnerId { init; get; }
    public required string TgToken { init; get; }

    public required bool HasAuth { init; get; }
    public string? OauthClient { init; get; }
    public string? OauthSecret { init; get; }
    public string? OauthHost { init; get; }
}