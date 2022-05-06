using System.Security.Claims;
using Core.Domains.User.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Dto;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : Controller
{
    private readonly IAuthService _authenticationService;

    public AuthController(IAuthService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [HttpPost("Login")]
    public async Task Login(LoginDto loginDto)
    {
        await _authenticationService.LoginAsync(loginDto.Email, loginDto.Password);
        await Authenticate(loginDto.Email);
    }

    [HttpPost("Register")]
    public async Task Register(RegisterDto registerDto)
    {
        await _authenticationService.RegisterAsync(registerDto.Nickname, registerDto.Email, registerDto.Password);
        await Authenticate(registerDto.Email);
    }

    private async Task Authenticate(string email)
    {
        var claims = new List<Claim> { new(ClaimsIdentity.DefaultNameClaimType, email) };
        var id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
    }

    [Authorize]
    [HttpPut("Logout")]
    public async Task Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    }
}