using System.Net;
using KebabFury.Innopolice.WebApi.Application.Dto.Provider;
using KebabFury.Innopolice.WebApi.Application.Services;
using KebabFury.Innopolice.WebApi.Application.Services.Interfaces;
using KebabFury.Innopolice.WebApi.Domain.Models;
using KebabFury.Innopolice.WebApi.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace KebabFury.Innopolice.WebApi.Controllers;

[Route("provider")]
public class ProviderController : BaseController<CustomProvider>
{
    private readonly IProviderService _providerService;

    public ProviderController(IProviderService providerService) : base(providerService)
    {
        _providerService = providerService;
    }
    
    [HttpGet("list-docs")]
    public Task<IList<CustomProviderDocumentationDto>> ListAllDocumentations()
    {
        return _providerService.ListAllDocumentations();
    }

    [HttpGet("{providerName}/authorize")]
    public Task<AuthorizeResultDto> Authorize(string providerName)
    {
        return _providerService.Authorize(providerName);
    }

    [HttpGet("{providerName}/get-token")]
    public async Task<IActionResult> Callback(string providerName, [FromQuery] string? code = null, [FromQuery] string? state = null, [FromQuery] string? error = null)
    {
        if (error is not null)
        {
            return BadRequest(error);
        }

        try
        {
            var result = await _providerService.CallbackAsync(providerName, code, state);
            return await _providerService.SaveAuthorizationDataAndReturnResponse(result, providerName);
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
        await _providerService.CreateCustomAsync(request);
    }

    [HttpPost("create-task")]
    public async Task<JsonResult> CreateTask(CreateTaskRequest request)
    {
        return await TaskService.CreateTask(request);
    }
    
}