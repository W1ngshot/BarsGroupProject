using Core;
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
    private readonly ICommentService _commentService;

    public ArticleController(IArticleService articleService,
        IUserService userService,
        IRateService rateService,
        IArticleCategoryService articleCategoryService, 
        ICommentService commentService)
    {
        _articleService = articleService;
        _userService = userService;
        _rateService = rateService;
        _articleCategoryService = articleCategoryService;
        _commentService = commentService;
    }

    [HttpGet("{articleId:int}")]
    public async Task<ArticlePageModel> Index(int articleId)
    {
        var articleModel = new ArticlePageModel
        {
            Article = await _articleService.GetArticleByIdAsync(articleId),
            Comments = await _commentService.GetArticleCommentsAsync(articleId)
        };
        return articleModel;
    }

    [Authorize]
    [HttpPost("Create")]
    public async Task Create(ArticleDto articleDto)
    {
        var nickname = HttpContext.Items["UserNickname"]?.ToString() ?? throw new AuthorizationException();
        var userId = await _userService.GetUserIdByNicknameAsync(nickname);
        await _articleService.CreateArticleAsync(articleDto.Title, articleDto.Text, userId,
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


    //TODO добавить проверку, что пользователь является владельцем статьи
    [Authorize]
    [HttpGet("Edit/{articleId:int}")]
    public async Task<Article> Edit(int articleId)
    {
        return await _articleService.GetArticleByIdAsync(articleId);
    }

    //TODO добавить проверку, что пользователь является владельцем статьи
    [Authorize]
    [HttpPut("Edit")]
    public async Task Edit(int articleId, ArticleDto article)
    {
        await _articleService.EditArticleAsync(articleId, article.Title, article.Text, article.ShortDescription,
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