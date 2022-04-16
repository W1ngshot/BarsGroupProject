using Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Dto;

namespace Web.Controllers;

public class ArticleController : Controller
{
    private readonly IArticleService _articleService;
    private readonly IUserService _userService;

    public ArticleController(IArticleService articleService, IUserService userService)
    {
        _articleService = articleService;
        _userService = userService;
    }

    //Возможно стоит возвращать Article? делать проверку на null и кидать View с 404
    public async Task<IActionResult> Index(int articleId)
    {
        var article = await _articleService.GetArticleByIdAsync(articleId);
        return View(article);
    }

    [Authorize]
    public IActionResult Create()
    {
        return View();
    }

    [Authorize]
    public async Task<IActionResult> Create(ArticleDto article)
    {
        var user = User.Identity?.Name ?? throw new Exception("Ошибка авторизации");
        var currentUserId = await _userService.GetUserIdByEmailAsync(user);
        await _articleService.CreateArticleAsync(article.Title, article.ShortDescription, article.Text, article.PictureLink, currentUserId);
        return View(article); //TODO подумать, куда перенаправить пользователя (возможно на эту статью)
    }

    //TODO добавить проверку, что пользователь является владельцем статьи
    [Authorize]
    public async Task<IActionResult> Edit(int articleId)
    {
        var article = await _articleService.GetArticleByIdAsync(articleId);
        return View(article);
    }

    //TODO добавить проверку, что пользователь является владельцем статьи
    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Edit(int articleId, ArticleDto article)
    {
        await _articleService.EditArticleAsync(articleId, article.Title, article.ShortDescription, article.Text, article.PictureLink);
        return View(article); //TODO подумать, куда перенаправить пользователя (возможно на эту статью)
    }
}