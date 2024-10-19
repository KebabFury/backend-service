using KebabFury.Innopolice.WebApi.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace KebabFury.Innopolice.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class BaseController<TEntity> : ControllerBase where TEntity : class
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

    [HttpGet("get-all")]
    public async Task<IList<TEntity>> GetAll()
    {
        return await _baseService.GetAllAsync();
    }

    [HttpDelete("{id:guid}")]
    public async Task Delete(Guid id)
    {
        await _baseService.DeleteAsync(id);
    }

}