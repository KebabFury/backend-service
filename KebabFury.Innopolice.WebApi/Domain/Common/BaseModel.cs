using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace KebabFury.Innopolice.WebApi.Domain.Common;

public class BaseModel
{
    [BsonId]
    [BsonGuidRepresentation(GuidRepresentation.Standard)]
    public Guid Id { get; set; }
}