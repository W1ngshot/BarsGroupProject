using Core;
using Core.Models;
using Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Dto;

namespace WebApi.Controllers;

public class ArticleController : Controller
{
    private readonly IArticleService _articleService;
    private readonly IUserService _userService;

    public ArticleController(IArticleService articleService, IUserService userService)
    {
        _articleService = articleService;
        _userService = userService;
    }

    public async Task<Article> Index(int articleId)
    {
        return await _articleService.GetArticleByIdAsync(articleId);
    }

    [Authorize]
    public async Task Create(ArticleDto articleDto)
    {
        var user = User.Identity?.Name ?? throw new Exception(ErrorMessages.AuthError);
        var currentUserId = await _userService.GetUserIdByEmailAsync(user);
        await _articleService.CreateArticleAsync(articleDto.Title, articleDto.Text, currentUserId,
            articleDto.ShortDescription, articleDto.PictureLink);
    }

    //TODO добавить проверку, что пользователь является владельцем статьи
    [Authorize]
    public async Task<Article> Edit(int articleId)
    {
        return await _articleService.GetArticleByIdAsync(articleId);
    }

    //TODO добавить проверку, что пользователь является владельцем статьи
    [Authorize]
    [HttpPost]
    public async Task Edit(int articleId, ArticleDto article)
    {
        await _articleService.EditArticleAsync(articleId, article.Title, article.Text, article.ShortDescription,
            article.PictureLink);
    }
}