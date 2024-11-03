using System.IdentityModel.Tokens.Jwt;
using KebabFury.Innopolice.WebApi.Domain.Models;
using KebabFury.Innopolice.WebApi.Infrastructure.Repositories;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using KebabFury.Innopolice.WebApi.Application.Dto.User;
using KebabFury.Innopolice.WebApi.Application.Services.Interfaces;
using KebabFury.Innopolice.WebApi.Application.Settings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace KebabFury.Innopolice.WebApi.Application.Services;

public class UserService : BaseService<User>, IUserService
{
    private readonly UserRepository _userRepository;
    private readonly IConfiguration _configuration;

    public UserService(UserRepository userRepository, IConfiguration configuration) : base(userRepository)
    {
        _userRepository = userRepository;
        _configuration = configuration;
    }

    public async Task<LoginResultDto> LoginAsync(LoginDto loginDto)
        {
            var user = await _userRepository.GetByEmailAsync(loginDto.Email);
            if (user == null)
            {
                throw new Exception("User not found!");
            }
            
            if (GetPasswordHash(loginDto.Password) == user.Password)
            {

                var loginResult = new LoginResultDto
                {
                    Id = user.Id,
                    AccessToken = GenerateJwt(user),
                    Name = user.Name,
                    Email = user.Email
                };
                return loginResult;
            }

            throw new Exception("Nickname or Password are not correct");
        }

        public async Task<bool> RegisterAsync(RegisterDto registerDto)
        {
            var userExistsCheck = await _userRepository.GetByEmailAsync(registerDto.Email);
            if (userExistsCheck != null)
            {
                throw new Exception("User already exists");
            }

            var user = new User()
            {
                Name = registerDto.Name,
                Email = registerDto.Email,
                Password = GetPasswordHash(registerDto.Password)
            };

            await _userRepository.AddEntityAsync(user);
            return true;
        }

        public async Task<bool> RegisterDefault(Guid id, RegisterDto registerDto)
        {
            var userExistsCheck = await _userRepository.GetByEmailAsync(registerDto.Email);
            if (userExistsCheck != null)
            {
                throw new Exception("User already exists");
            }

            var user = new User()
            {
                Id = id,
                Name = registerDto.Name,
                Email = registerDto.Email,
                Password = GetPasswordHash(registerDto.Password)
            };

            await _userRepository.AddEntityAsync(user);
            return true;
        }

        private string GenerateJwt(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email),
            };
            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddDays(7),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private string GetPasswordHash(string password)
        {
            var sha = SHA256.Create();
            var byteArray = Encoding.UTF8.GetBytes(password);
            var hash = sha.ComputeHash(byteArray);
            return Convert.ToBase64String(hash);
        }
}