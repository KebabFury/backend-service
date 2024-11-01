namespace KebabFury.Innopolice.Todoist.Models;

public sealed record Due
{
    public required string? String;
    public required DateTime? Date;
    public required bool IsRecurring;
    public required DateTime? DateTime;
    public required string? Timezone;
}
