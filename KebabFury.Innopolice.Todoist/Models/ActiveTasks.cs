namespace KebabFury.Innopolice.Todoist.Models;

public sealed record ActiveTasks
{
    public required List<TodoistTask> Result;
}
