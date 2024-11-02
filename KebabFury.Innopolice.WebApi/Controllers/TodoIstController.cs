using System.Text.Json;
using KebabFury.Innopolice.TodoIst.Services;
using KebabFury.Innopolice.WebApi.Application.Dto.TodoIst;
using KebabFury.Innopolice.WebApi.Application.Services.Providers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace KebabFury.Innopolice.WebApi.Controllers;

[ApiController]
[Route("todoist")]
public class TodoIstController : ControllerBase
{
    private readonly ITodoIstAuthorizationService _todoIstAuthorizationService;
    private readonly IProviderService _providerService;

    public TodoIstController(
        ITodoIstAuthorizationService todoIstAuthorizationService,
        IProviderService providerService)
    {
        _todoIstAuthorizationService = todoIstAuthorizationService;
        _providerService = providerService;
    }

    [HttpGet("authorize")]
    public Task<string> GetAuthorization()
    {
        return Task.FromResult(_todoIstAuthorizationService.Authorize());
    } 
    
    [HttpGet("get-token")]
    public async Task<IActionResult> GetTodoistToken([FromQuery] string code = null, [FromQuery] string state = null, [FromQuery] string error = null)
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
    
}