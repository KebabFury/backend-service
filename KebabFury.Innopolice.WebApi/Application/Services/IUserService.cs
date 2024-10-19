using KebabFury.Innopolice.WebApi.Application.Dto;
using KebabFury.Innopolice.WebApi.Domain.Models;

namespace KebabFury.Innopolice.WebApi.Application.Services;

public interface IUserService : IBaseService<User>
{
    public Task<LoginResultDto> LoginAsync(LoginDto loginDto);
    public Task<bool> RegisterAsync(RegisterDto registerDto);
}