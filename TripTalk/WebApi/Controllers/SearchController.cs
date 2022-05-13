using Core.Domains.Article.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using WebApi.Dto;
using WebApi.Models;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class SearchController : Controller
{
    private readonly ISearchService _searchService;
    private readonly IArticleService _articleService;
    private const int ArticlesOnPage = 6;

    public SearchController(ISearchService searchService, IArticleService articleService)
    {
        _searchService = searchService;
        _articleService = articleService;
    }

    [HttpGet("")]
    public async Task<ArticlesModel> Index(string q, string? tagsString, int pageNumber = 1)
    {
        var tags = tagsString?.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList()
                   ?? new List<string>();
        var firstElementIndex = ArticlesOnPage * (pageNumber - 1);
        return new ArticlesModel
        {
            Articles = await _searchService.FindArticlesAsync(q, tags,
                ArticlesOnPage, firstElementIndex),
            TotalCount = await _articleService.GetFilteredArticlesCountAsync(q, tags)
        };
    }
}