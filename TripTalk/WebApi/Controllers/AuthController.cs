using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Core.Domains.User.Services.Interfaces;
using WebApi.Dto;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : Controller
{
    private readonly IAuthService _authenticationService;
    private readonly IConfiguration _configuration;

    public AuthController(IAuthService authenticationService, IConfiguration configuration)
    {
        _authenticationService = authenticationService;
        _configuration = configuration;
    }

    [HttpPost("Login")]
    public async Task<string> Login(LoginDto loginDto)
    {
        await _authenticationService.LoginAsync(loginDto.Nickname, loginDto.Password);
        return GenerateJwtToken(loginDto.Nickname);
    }

    [HttpPost("Register")]
    public async Task<string> Register(RegisterDto registerDto)
    {
        await _authenticationService.RegisterAsync(registerDto.Nickname, registerDto.Email, registerDto.Password, registerDto.ConfirmPassword);
        return GenerateJwtToken(registerDto.Nickname);
    }

    private string GenerateJwtToken(string nickname)
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
}