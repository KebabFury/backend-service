using KebabFury.Innopolice.WebApi.Domain.Common;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace KebabFury.Innopolice.WebApi.Domain.Models;

public sealed class BotUser : BaseModel
{
    [BsonGuidRepresentation(GuidRepresentation.Standard)]
    public required Guid BotId { get; set; }
    public Authorization? Authorization { get; set; }
}