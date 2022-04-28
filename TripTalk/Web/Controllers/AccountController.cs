using Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Dto;

namespace Web.Controllers;

[Authorize]
public class AccountController : Controller
{
    private readonly IUserService _userService;
    private readonly IArticleService _articleService;

    public AccountController(IUserService userService, IArticleService articleService)
    {
        _userService = userService;
        _articleService = articleService;
    }

    public async Task<IActionResult> Index()
    {
        var email = User.Identity?.Name ?? throw new Exception("Ошибка авторизации");
        var user = await _userService.GetUserByEmailAsync(email);
        return View(user);
    }

    public async Task<IActionResult> MyArticles()
    {
        var email = User.Identity?.Name ?? throw new Exception("Ошибка авторизации");
        var userId = await _userService.GetUserIdByEmailAsync(email);
        var articles = await _articleService.GetUserArticlesAsync(userId);
        return View(articles);
    }


    public IActionResult Logout()
    {
        return RedirectToAction("Logout", "Auth");
    }

    public IActionResult ChangePassword()
    {
        return View();
    }

    public IActionResult ChangePassword(ChangePasswordDto changePasswordModel)
    {
        var email = User.Identity?.Name ?? throw new Exception("Ошибка авторизации");
        _userService.ChangePasswordAsync(email, changePasswordModel.OldPassword, changePasswordModel.NewPassword);
        return RedirectToAction("Index");
    }
}