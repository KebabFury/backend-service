using KebabFury.Innopolice.WebApi.Application.Dto.Provider;
using KebabFury.Innopolice.WebApi.Application.Services.Interfaces;
using KebabFury.Innopolice.WebApi.Domain.Models;
using KebabFury.Innopolice.WebApi.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace KebabFury.Innopolice.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class ProviderController : ControllerBase
{
    private readonly IProviderService _providerService;

    public ProviderController(IProviderService providerService)
    {
        _providerService = providerService;
    }

    [HttpPost("create")]
    public async Task Create([FromBody] CreateCustomProviderRequest request)
    {
        await _providerService.CreateCustomAsync(request);
    }

    [HttpGet("list-docs")]
    public Task<IList<CustomProviderDocumentationDto>> ListAllDocumentations()
    {
        return _providerService.ListAllDocumentations();
    }
}