using KebabFury.Innopolice.Todoist.Dto.Requests;
using KebabFury.Innopolice.Todoist.Services;
using KebabFury.Innopolice.WebApi.Application.Dto.Provider;
using KebabFury.Innopolice.WebApi.Application.Dto.TodoIst;
using KebabFury.Innopolice.WebApi.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace KebabFury.Innopolice.WebApi.Controllers;

[ApiController]
[Route("todoist")]
public class TodoIstController : ControllerBase
{
    private readonly ITodoIstAuthorizationService _todoIstAuthorizationService;
    private readonly ICustomProviderAuthorizationService _providerAuthorizationService;
    private readonly ITodoistService _todoistService;

    public TodoIstController(
        ITodoIstAuthorizationService todoIstAuthorizationService,
        ICustomProviderAuthorizationService providerAuthorizationService,
        ITodoistService todoistService
        )
    {
        _todoIstAuthorizationService = todoIstAuthorizationService;
        _providerAuthorizationService = providerAuthorizationService;
        _todoistService = todoistService;
    }

    [HttpGet("authorize")]
    public Task<AuthorizeResultDto> GetAuthorization()
    {
        var authorizationData = _todoIstAuthorizationService.Authorize();
        return Task.FromResult(new AuthorizeResultDto(authorizationData));
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

        return await _providerAuthorizationService.SaveAuthorizationDataAndReturnResponse(authorizationToken, "Todoist");
    }

    [HttpPost("create_task")]
    public async Task<IActionResult> CreateTask([FromQuery] string accessToken, [FromBody] TaskCreateRequest request)
    {
        return Ok(await this._todoistService.CreateTask(request, accessToken));
    }
}
