using Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApi.Dto;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : Controller
{
    private readonly IAuthService _authenticationService;
    private readonly IConfiguration _configuration;
    private readonly IUserService _userService;

    public AuthController(IAuthService authenticationService, IConfiguration configuration, IUserService userService)
    {
        _authenticationService = authenticationService;
        _configuration = configuration;
        _userService = userService;
    }

    [HttpPost("Login")]
    public async Task<string> Login(LoginDto loginDto)
    {
        var userId = await _authenticationService.LoginAsync(loginDto.Nickname, loginDto.Password);
        return GenerateJwtToken(loginDto.Nickname, userId);
    }

    [HttpPost("Register")]
    public async Task<string> Register(RegisterDto registerDto)
    {
        await _authenticationService.RegisterAsync(registerDto.Nickname, registerDto.Email, registerDto.Password);
        var userId = await _userService.GetUserIdByNicknameAsync(registerDto.Nickname);
        return GenerateJwtToken(registerDto.Nickname, userId);
    }

    private string GenerateJwtToken(string nickname, int userId)
    {
        var key = Encoding.ASCII.GetBytes(_configuration["Jwt:key"]);
        var jwt = new JwtSecurityToken(
            claims: new List<Claim> { new("Name", nickname) },
            expires: DateTime.UtcNow.AddHours(1),
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature));
        return new JwtSecurityTokenHandler().WriteToken(jwt);
    }

    [Authorize]
    [HttpPut("Logout")]
    public async Task Logout()
    {
        //TODO ???
    }
}