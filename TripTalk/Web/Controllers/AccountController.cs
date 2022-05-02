using Core;
using Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OldWeb.Dto;
using OldWeb.Models;

namespace OldWeb.Controllers;

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
        var email = User.Identity?.Name ?? throw new Exception(ErrorMessages.AuthError);
        var user = await _userService.GetUserByEmailAsync(email);
        return View(user);
    }

    public async Task<IActionResult> MyAccount()
    {
        var email = User.Identity?.Name ?? throw new Exception(ErrorMessages.AuthError);
        var user = await _userService.GetUserByEmailAsync(email);
        var userProfileModel = new UserProfileModel
        {
            User = user,
            Articles = await _articleService.GetUserArticlesAsync(user.Id, 4)
        };
        return View(userProfileModel);
    }

    public async Task<IActionResult> MyArticles()
    {
        var email = User.Identity?.Name ?? throw new Exception(ErrorMessages.AuthError);
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
        var email = User.Identity?.Name ?? throw new Exception(ErrorMessages.AuthError);
        _userService.ChangePasswordAsync(email, changePasswordModel.OldPassword, changePasswordModel.NewPassword);
        return RedirectToAction("Index");
    }
}