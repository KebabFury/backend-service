using System.Net;
using KebabFury.Innopolice.WebApi.Application.Dto.Provider;
using KebabFury.Innopolice.WebApi.Application.Services;
using KebabFury.Innopolice.WebApi.Application.Services.Interfaces;
using KebabFury.Innopolice.WebApi.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace KebabFury.Innopolice.WebApi.Controllers;

[Route("provider")]
public class CustomProviderController : BaseController<CustomProvider>
{
    private readonly ICustomProviderService _customProviderService;
    private readonly ICustomProviderAuthorizationService _customProviderAuthorizationService;

    public CustomProviderController(
        ICustomProviderService customProviderService,
        ICustomProviderAuthorizationService customProviderAuthorizationService) : base(customProviderService)
    {
        _customProviderService = customProviderService;
        _customProviderAuthorizationService = customProviderAuthorizationService;
    }

    [HttpGet("by-user-id/{userId:guid}")]
    public async Task<IList<CustomProvider>> ListByUserId([FromRoute] Guid userId)
    {
        return await _customProviderService.ListByUserId(userId);
    }

    [HttpGet("list-docs")]
    public Task<IList<CustomProviderDocumentationDto>> ListAllDocumentations()
    {
        return _customProviderService.ListAllDocumentations();
    }

    [HttpGet("{providerName}/authorize")]
    public Task<AuthorizeResultDto> Authorize(string providerName)
    {
        return _customProviderAuthorizationService.Authorize(providerName);
    }

    [HttpGet("{providerName}/get-token")]
    public async Task<IActionResult> Callback(string providerName, [FromQuery] string? code = null, [FromQuery] string? error = null)
    {
        if (error is not null)
        {
            return BadRequest(error);
        }

        try
        {
            var result = await _customProviderAuthorizationService.CallbackAsync(providerName, code);
            return await _customProviderAuthorizationService.SaveAuthorizationDataAndReturnResponse(result, providerName);
        }
        catch (HttpRequestException ex)
        {
            return ex.StatusCode switch
            {
                HttpStatusCode.Unauthorized => Unauthorized(ex.Message),
                HttpStatusCode.NotFound => NotFound(ex.Message),
                _ => BadRequest(ex.Message)
            };
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("create-provider")]
    public async Task Create([FromBody] CreateCustomProviderRequest request)
    {
        await _customProviderService.CreateCustomProviderAsync(request);
    }

    [HttpPost("handle-provider-endpoint")]
    public async Task<JsonResult> CreateTask(CreateTaskRequest request)
    {
        return await ProviderEndpointHandleService.HandleAsync(request);
    }

    [HttpPut("{id:guid}/deep-upadte")]
    public async Task DeepUpdate(Guid id, [FromBody] CreateCustomProviderRequest request)
    {
        await _customProviderService.DeepUpdateAsync(id, request);
    }

    [HttpDelete("delete-all")]
    public async Task DeleteAll()
    {
        await _customProviderService.DeleteAll();
    }
}
