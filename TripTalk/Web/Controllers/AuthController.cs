using Core.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Web.Dto;

namespace Web.Controllers;

public class AuthController : Controller
{
    private readonly IAuthService _authenticationService;

    public AuthController(IAuthService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginDto model)
    {
        if (ModelState.IsValid)
        {
            await _authenticationService.LoginAsync(model.Email, model.Password); //TODO придумать как работать с исключениями
            await Authenticate(model.Email);

            return RedirectToAction("Index", "Home");
            //ModelState.AddModelError("", "Некорректные логин и(или) пароль");
        }
        return View(model);
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterDto model)
    {
        if (ModelState.IsValid)
        {
            await _authenticationService.RegisterAsync(model.NickName, model.Email, model.Password); //TODO придумать как работать с исключениями
            await Authenticate(model.Email);

            return RedirectToAction("Index", "Home");
            //ModelState.AddModelError("", "Некорректные логин и(или) пароль");
        }
        return View(model);
    }

    private async Task Authenticate(string userName)
    {
        var claims = new List<Claim> { new(ClaimsIdentity.DefaultNameClaimType, userName) };
        var id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
    }

    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Login", "Auth");
    }
}