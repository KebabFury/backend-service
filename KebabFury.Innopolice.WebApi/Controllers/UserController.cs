using KebabFury.Innopolice.WebApi.Application.Dto.User;
using KebabFury.Innopolice.WebApi.Application.Services.Interfaces;
using KebabFury.Innopolice.WebApi.Application.Settings;
using KebabFury.Innopolice.WebApi.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace KebabFury.Innopolice.WebApi.Controllers;

public class UserController : BaseController<User>
{
    private readonly IUserService _userService;
    private readonly DefaultUserAccount _defaultUserAccount;

    public UserController(
        IOptions<DefaultUserAccount> defaultUserAccount,
        IUserService userService) : base(userService)
    {
        _defaultUserAccount = defaultUserAccount.Value;
        _userService = userService;
    }

    [HttpGet("default-account/login")]
    public async Task<IActionResult> GetDefaultAccount()
    {
        try
        {
            var loginDto = new LoginDto()
            {
                Email = _defaultUserAccount.Email,
                Password = _defaultUserAccount.Password
            };
            
            var loginResult = await _userService.LoginAsync(loginDto);
            return Ok(loginResult);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("default-account/register")]
    public async Task<IActionResult> RegisterDefaultAccount()
    {
        try
        {
            var registerDto = new RegisterDto
            {
                Name = _defaultUserAccount.Name,
                Email = _defaultUserAccount.Email,
                Password = _defaultUserAccount.Password
            };
            return Ok(await _userService.RegisterDefault(new Guid(_defaultUserAccount.Id), registerDto));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        try
        {
            var loginResult = await _userService.LoginAsync(loginDto);
            return Ok(loginResult);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
    {
        try
        {
            var result = await _userService.RegisterAsync(registerDto);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
