using Core;
using Core.CustomExceptions;
using Core.Models;
using Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Dto;
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
        return new UserProfileModel
        {
            User = await _userService.GetUserByIdAsync(id),
            Articles = await _articleService.GetUserArticlesAsync(id, 4)
        };
    }

    [HttpGet("MyAccount")]
    public async Task<UserProfileModel> MyAccount()
    {
        var email = User.Identity?.Name ?? throw new ValidationException(ErrorMessages.AuthError);
        var user = await _userService.GetUserByEmailAsync(email);
        return new UserProfileModel
        {
            User = user,
            Articles = await _articleService.GetUserArticlesAsync(user.Id, 4)
        };
    }

    [HttpGet("MyArticles")]
    public async Task<List<Article>> MyArticles(int pageNumber = 1)
    {
        var email = User.Identity?.Name ?? throw new Exception(ErrorMessages.AuthError);
        var userId = await _userService.GetUserIdByEmailAsync(email);

        var firstElementIndex = ArticlesOnPage * (pageNumber - 1);

        return await _articleService.GetUserArticlesAsync(userId, ArticlesOnPage, firstElementIndex);
    }

    [HttpPut("ChangePassword")]
    public async Task ChangePassword(ChangePasswordDto changePasswordDto)
    {
        var email = User.Identity?.Name ?? throw new Exception(ErrorMessages.AuthError);
        await _userService.ChangePasswordAsync(email, changePasswordDto.OldPassword, changePasswordDto.NewPassword,
            changePasswordDto.ConfirmNewPassword);
    }
}