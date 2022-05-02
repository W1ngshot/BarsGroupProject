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
public class AccountController : Controller
{
    private readonly IUserService _userService;
    private readonly IArticleService _articleService;

    public AccountController(IUserService userService, IArticleService articleService)
    {
        _userService = userService;
        _articleService = articleService;
    }

    public async Task<UserProfileModel> MyAccount()
    {
        var email = User.Identity?.Name ?? throw new ValidationException(ErrorMessages.AuthError);
        var user = await _userService.GetUserByEmailAsync(email);
        var userProfileModel = new UserProfileModel
        {
            User = user,
            Articles = await _articleService.GetUserArticlesAsync(user.Id, 4)
        };
        return userProfileModel;
    }

    //TODO настроить количество для страниц
    public async Task<List<Article>> MyArticles()
    {
        var email = User.Identity?.Name ?? throw new Exception(ErrorMessages.AuthError);
        var userId = await _userService.GetUserIdByEmailAsync(email);
        return await _articleService.GetUserArticlesAsync(userId);
    }

    public async Task ChangePassword(ChangePasswordDto changePasswordDto)
    {
        var email = User.Identity?.Name ?? throw new Exception(ErrorMessages.AuthError);
        await _userService.ChangePasswordAsync(email, changePasswordDto.OldPassword, changePasswordDto.NewPassword,
            changePasswordDto.ConfirmNewPassword);
    }
}