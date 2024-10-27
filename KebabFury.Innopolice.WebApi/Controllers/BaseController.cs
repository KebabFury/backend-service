using System.Security.Claims;
using KebabFury.Innopolice.WebApi.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace KebabFury.Innopolice.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class BaseController<TEntity> : ControllerBase
    where TEntity : class
{
    private readonly IBaseService<TEntity> _baseService;

    public BaseController(IBaseService<TEntity> baseService)
    {
        _baseService = baseService;
    }

    [HttpGet("{id:guid}")]
    public async Task<TEntity> GetById(Guid id)
    {
        return await _baseService.GetByIdAsync(id);
    }

    [HttpGet]
    public async Task<IList<TEntity>> GetAll()
    {
        return await _baseService.GetAllAsync();
    }

    [HttpDelete("{id:guid}")]
    public async Task Delete(Guid id)
    {
        await _baseService.DeleteAsync(id);
    }

    [HttpPut]
    public async Task Update([FromBody] TEntity entity)
    {
        await _baseService.UpdateAsync(entity);
    }

    protected Guid GetAccountId()
    {
        var claimValue = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(claimValue) || !Guid.TryParse(claimValue, out var accountId))
        {
            throw new UnauthorizedAccessException();
        }

        return accountId;
    }
}
