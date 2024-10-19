using KebabFury.Innopolice.WebApi.Domain.Common;

namespace KebabFury.Innopolice.WebApi.Domain.Models;

public sealed class User : BaseModel
{
    public required string Email { get; set; }
    public required string Password { get; set; }
}