using Core.Services;
using Microsoft.AspNetCore.Mvc;
using WebApi.Dto;
using WebApi.Models;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class SearchController : Controller
{
    private readonly ISearchService _searchService;
    private const int ArticlesOnPage = 6;

    public SearchController(ISearchService searchService)
    {
        _searchService = searchService;
    }

    [HttpGet("Search")]
    public async Task<SearchModel> Index(SearchDto searchDto)
    {
        var firstElementIndex = ArticlesOnPage * (searchDto.PageNumber - 1);
        var searchModel = new SearchModel
        {
            SearchedArticles = await _searchService.FindArticlesAsync(searchDto.Text, searchDto.Tags,
                ArticlesOnPage, firstElementIndex)
        };
        return searchModel;
    }
}