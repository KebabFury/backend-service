using KebabFury.Innopolice.Todoist.Dto.Requests;
using KebabFury.Innopolice.Todoist.Dto.Responses;

namespace KebabFury.Innopolice.TodoIst.Services;

public interface ITodoistService
{
    public Task<TaskCreateResponse> CreateTask(TaskCreateRequest request);
}
