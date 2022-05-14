using Core.CustomExceptions;
using Core.Domains.Article;
using Core.Domains.Article.Services.Interfaces;
using Core.Domains.Comment.Services.Interfaces;
using Core.Domains.Rate;
using Core.Domains.Rate.Services.Interfaces;
using Core.Domains.User.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Dto;
using WebApi.Models;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class ArticleController : Controller
{
    private const int ArticlesOnPage = 6;
    private readonly IArticleService _articleService;
    private readonly IUserService _userService;
    private readonly IRateService _rateService;
    private readonly IArticleCategoryService _articleCategoryService;

    public ArticleController(IArticleService articleService,
        IUserService userService,
        IRateService rateService,
        IArticleCategoryService articleCategoryService)
    {
        _articleService = articleService;
        _userService = userService;
        _rateService = rateService;
        _articleCategoryService = articleCategoryService;
    }

    [HttpGet("{articleId:int}")]
    public async Task<Article> Index(int articleId)
    {
        return await _articleService.GetArticleByIdAsync(articleId);
    }

    [Authorize]
    [HttpPost("Create")]
    public async Task<Article> Create(ArticleDto articleDto)
    {
        var nickname = HttpContext.Items["UserNickname"]?.ToString() ?? throw new AuthorizationException();
        var userId = await _userService.GetUserIdByNicknameAsync(nickname);
        return await _articleService.CreateArticleAsync(articleDto.Title, articleDto.Text, userId,
            articleDto.ShortDescription, articleDto.PictureLink, articleDto.Tags);
    }

    [Authorize]
    [HttpPost("AddRate")]
    public async Task AddRate(int articleId, Rate rate)
    {
        var nickname = HttpContext.Items["UserNickname"]?.ToString() ?? throw new AuthorizationException();
        var userId = await _userService.GetUserIdByNicknameAsync(nickname);
        await _rateService.SetRateAsync(userId, articleId, rate);
    }

    [HttpGet("GetCurrentUserRate")]
    public async Task<int> GetCurrentUserRate(int articleId)
    {
        var nickname = HttpContext.Items["UserNickname"]?.ToString();
        if (nickname == null)
            return 0;

        var userId = await _userService.GetUserIdByNicknameAsync(nickname);
        return await _rateService.GetRateAsync(userId, articleId);
    }

    [Authorize]
    [HttpGet("Edit/{articleId:int}")]
    public async Task<Article> Edit(int articleId)
    {
        var nickname = HttpContext.Items["UserNickname"]?.ToString() ?? throw new AuthorizationException();
        var userId = await _userService.GetUserIdByNicknameAsync(nickname);
        await _articleService.EnsureArticleAuthorshipAsync(userId, articleId);

        return await _articleService.GetArticleByIdAsync(articleId);
    }

    [HttpDelete("Delete/{articleId:int}")]
    public async Task Delete(int articleId)
    {
        var nickname = HttpContext.Items["UserNickname"]?.ToString() ?? throw new AuthorizationException();
        var userId = await _userService.GetUserIdByNicknameAsync(nickname);
        await _articleService.EnsureArticleAuthorshipAsync(userId, articleId);

        await _articleService.DeleteArticleAsync(articleId);
    }

    [Authorize]
    [HttpPut("Edit")]
    public async Task<Article> Edit(int articleId, ArticleDto article)
    {
        var nickname = HttpContext.Items["UserNickname"]?.ToString() ?? throw new AuthorizationException();
        var userId = await _userService.GetUserIdByNicknameAsync(nickname);
        await _articleService.EnsureArticleAuthorshipAsync(userId, articleId);

        return await _articleService.EditArticleAsync(articleId, article.Title, article.Text, article.ShortDescription,
            article.PictureLink, article.Tags);
    }

    [HttpGet("Popular")]
    public async Task<ArticlesModel> PopularArticles(Period period = Period.AllTime, int pageNumber = 1) => 
        await GetCategoryArticles(Category.Popular, period, pageNumber);

    [HttpGet("Best")]
    public async Task<ArticlesModel> BestArticles(Period period = Period.AllTime, int pageNumber = 1) =>
        await GetCategoryArticles(Category.Best, period, pageNumber);

    [HttpGet("Latest")]
    public async Task<ArticlesModel> LatestArticles(Period period = Period.AllTime, int pageNumber = 1) =>
        await GetCategoryArticles(Category.Last, period, pageNumber);

    private async Task<ArticlesModel> GetCategoryArticles(Category category, Period period, int pageNumber)
    {
        var firstElementIndex = ArticlesOnPage * (pageNumber - 1);
        return new ArticlesModel
        {
            Articles = await _articleCategoryService.GetOrderedArticlesAsync(category, period,
                ArticlesOnPage, firstElementIndex),
            TotalCount = await _articleCategoryService.GetArticlesCount()
        };
    }
}