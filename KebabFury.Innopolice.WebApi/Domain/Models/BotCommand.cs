using KebabFury.Innopolice.WebApi.Domain.Common;
using KebabFury.Innopolice.WebApi.Domain.Types;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace KebabFury.Innopolice.WebApi.Domain.Models;

public sealed class BotCommand : BaseModel
{
    [BsonGuidRepresentation(GuidRepresentation.Standard)]
    public Guid BotId { get; set; }
    public bool NeedAuth { get; set; }
    public required string Description { get; set; }
    public required string RequestUrl { init; get; }
    public required RequestMethod RequestMethod { get; set; }
}