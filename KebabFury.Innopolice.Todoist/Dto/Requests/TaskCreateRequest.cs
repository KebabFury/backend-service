using KebabFury.Innopolice.Todoist.Models;

namespace KebabFury.Innopolice.Todoist.Dto.Requests;

public sealed record TaskCreateRequest
{
    public required string Content { get; set; }
    public required string? Description { get; set; }
    public required string? ProjectId { get; set; }
    public required List<string>? Labels { get; set; }
    public required int? Priority { get; set; } = 1;
    public required string? DueString { get; set; }
    public required string? DueLang { get; set; } = "en";
    public required DateTime? DueDate { get; set; }
    public required DateTime? DueDatetime { get; set; }
    public required Duration? Duration { get; set; }
    public required string? DurationUnit { get; set; }
}
