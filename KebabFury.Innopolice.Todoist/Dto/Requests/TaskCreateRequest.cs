using KebabFury.Innopolice.Todoist.Models;

namespace KebabFury.Innopolice.Todoist.Dto.Requests;

public sealed record TaskCreateRequest
{
    public required string Content;
    public required string? Description;
    public required string? ProjectId;
    public required List<string>? Labels;
    public required int? Priority = 1;
    public required string? DueString;
    public required string? DueLang = "en";
    public required DateTime? DueDate;
    public required DateTime? DueDatetime;
    public required Duration? Duration;
    public required string? DurationUnit;
}
