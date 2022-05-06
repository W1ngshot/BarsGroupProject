using Core;
using Core.CustomExceptions;
using Core.Domains.Article.Services.Interfaces;
using Core.Domains.User.Services.Interfaces;
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
        var nickname = HttpContext.Items["UserNickname"]?.ToString() ?? throw new AuthorizationException();
        var user = await _userService.GetUserByNicknameAsync(nickname);
        return new UserProfileModel
        {
            User = user.ToPublicUser(),
            Articles = await _articleService.GetUserArticlesAsync(user.Id, 4)
        };
    }

    [HttpGet("MyArticles")]
    public async Task<ArticlesModel> MyArticles(int pageNumber = 1)
    {
        var nickname = HttpContext.Items["UserNickname"]?.ToString() ?? throw new AuthorizationException();
        var userId = await _userService.GetUserIdByNicknameAsync(nickname);

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
        var nickname = HttpContext.Items["UserNickname"]?.ToString() ?? throw new AuthorizationException();
        await _userService.ChangePasswordAsync(nickname, changePasswordDto.OldPassword, changePasswordDto.NewPassword,
            changePasswordDto.ConfirmNewPassword);
    }
}