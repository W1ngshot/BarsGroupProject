using Core;
using Core.CustomExceptions;
using Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Dto;
using WebApi.ModelExtensions;
using WebApi.Models;

namespace WebApi.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class AccountController : Controller
{
    private const int ArticlesOnPage = 6;
    private readonly IUserService _userService;
    private readonly IArticleService _articleService;

    public AccountController(IUserService userService, IArticleService articleService)
    {
        _userService = userService;
        _articleService = articleService;
    }

    [AllowAnonymous]
    [HttpGet("{id:int}")]
    public async Task<UserProfileModel> Index(int id)
    {
        var user = await _userService.GetUserByIdAsync(id);
        return new UserProfileModel
        {
            User = user.ToPublicUser(),
            Articles = await _articleService.GetUserArticlesAsync(id, 4)
        };
    }

    [AllowAnonymous]
    [HttpGet("UserArticles/{userId:int}")]
    public async Task<ArticlesModel> UserArticles(int userId, int pageNumber = 1)
    {
        var firstElementIndex = ArticlesOnPage * (pageNumber - 1);
        return new ArticlesModel
        {
            Articles = await _articleService.GetUserArticlesAsync(userId, 6, firstElementIndex),
            TotalCount = await _articleService.GetUserArticlesCountAsync(userId)
        };
    }

    [HttpGet("MyAccount")]
    public async Task<UserProfileModel> MyAccount()
    {
        var email = User.Identity?.Name ?? throw new AuthorizationException();
        var user = await _userService.GetUserByEmailAsync(email);
        return new UserProfileModel
        {
            User = user.ToPublicUser(),
            Articles = await _articleService.GetUserArticlesAsync(user.Id, 4)
        };
    }

    [HttpGet("MyArticles")]
    public async Task<ArticlesModel> MyArticles(int pageNumber = 1)
    {
        var email = User.Identity?.Name ?? throw new AuthorizationException();
        var userId = await _userService.GetUserIdByEmailAsync(email);

        var firstElementIndex = ArticlesOnPage * (pageNumber - 1);

        return new ArticlesModel
        {
            Articles = await _articleService.GetUserArticlesAsync(userId, ArticlesOnPage, firstElementIndex),
            TotalCount = await _articleService.GetUserArticlesCountAsync(userId)
        };
    }

    [HttpPut("ChangePassword")]
    public async Task ChangePassword(ChangePasswordDto changePasswordDto)
    {
        var email = User.Identity?.Name ?? throw new AuthorizationException();
        await _userService.ChangePasswordAsync(email, changePasswordDto.OldPassword, changePasswordDto.NewPassword,
            changePasswordDto.ConfirmNewPassword);
    }
}