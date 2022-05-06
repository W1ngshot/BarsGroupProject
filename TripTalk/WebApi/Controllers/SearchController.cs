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
    public async Task<ArticlesModel> Index(SearchDto searchDto)
    {
        var firstElementIndex = ArticlesOnPage * (searchDto.PageNumber - 1);
        return new ArticlesModel
        {
            Articles = await _searchService.FindArticlesAsync(searchDto.Text, searchDto.Tags,
                ArticlesOnPage, firstElementIndex),
            TotalCount = await _articleService.GetFilteredArticlesCountAsync(searchDto.Text, searchDto.Tags)
        };
    }
}