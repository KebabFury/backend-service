namespace KebabFury.Innopolice.Todoist.Models;

public record TodoistTask
{
    public required string Id;
    public required string? AssignerId;
    public required string? AssigneeId;
    public required string ProjectId;
    public required string? SectionId;
    public required string? ParentId;
    public required int Order;
    public required string Content;
    public required string Description;
    public required bool IsCompleted;
    public required List<string>? Labels;
    public required int Priority = 1;
    public required int CommentCount;
    public required string? CreatorId;
    public required DateTime CreatedAt;
    public required Due? Due;
    public required string Url;
    public required Duration? Duration;
}
