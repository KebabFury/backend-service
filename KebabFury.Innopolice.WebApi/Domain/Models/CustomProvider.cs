using KebabFury.Innopolice.WebApi.Domain.Common;
using KebabFury.Innopolice.WebApi.Domain.Types;

namespace KebabFury.Innopolice.WebApi.Domain.Models;

public class CustomProvider : BaseModel
{
    public required string Name { get; set; }
    public required string ActionCode { get; set; }
    public required string Documentation { get; set; }
}