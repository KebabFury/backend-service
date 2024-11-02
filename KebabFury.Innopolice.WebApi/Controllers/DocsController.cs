using Microsoft.AspNetCore.Mvc;

namespace KebabFury.Innopolice.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public sealed class DocsController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Todoist()
    {
        return Ok();
    }
}
