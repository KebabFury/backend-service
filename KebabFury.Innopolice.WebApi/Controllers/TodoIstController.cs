using KebabFury.Innopolice.Todoist.Dto.Requests;
using KebabFury.Innopolice.TodoIst.Services;
using KebabFury.Innopolice.WebApi.Application.Dto.TodoIst;
using KebabFury.Innopolice.WebApi.Application.Services.Providers;
using Microsoft.AspNetCore.Mvc;

namespace KebabFury.Innopolice.WebApi.Controllers;

[ApiController]
[Route("todoist")]
public class TodoIstController : ControllerBase
{
    private readonly ITodoIstAuthorizationService _todoIstAuthorizationService;
    private readonly IProviderService _providerService;
    private readonly ITodoistService _todoistService;

    public TodoIstController(
        ITodoIstAuthorizationService todoIstAuthorizationService,
        IProviderService providerService,
        ITodoistService todoistService,
        )
    {
        _todoIstAuthorizationService = todoIstAuthorizationService;
        _providerService = providerService;
        _todoistService = todoistService;
    }

    [HttpGet("authorize")]
    public Task<TodoIstAuthorizeResult> GetAuthorization()
    {
        var authorizationData = _todoIstAuthorizationService.Authorize();
        return Task.FromResult(new TodoIstAuthorizeResult(authorizationData));
    }
    [HttpGet("get-token")]
    public async Task<IActionResult> GetTodoistToken([FromQuery] string? code = null, [FromQuery] string? state = null, [FromQuery] string? error = null)
    {
        switch (error)
        {
            case "invalid_application_status":
                return StatusCode(500, new { detail = "Invalid application status" });
            case "invalid_scope":
                return BadRequest(new { detail = "Invalid scope" });
            case "access_denied":
                return Forbid("User denied authorization");
        }

        var authorizationToken = await _todoIstAuthorizationService.CallbackAsync(code, state, error);

        return await _providerService.SaveAuthorizationDataAndReturnResponse(authorizationToken, "Todoist");
    }

    [HttpPost]
    public Task<IActionResult> CreateTask([FromBody] TaskCreateRequest request)
    {
        return Ok(this._todoistService.CreateTask(request));
    }
}
