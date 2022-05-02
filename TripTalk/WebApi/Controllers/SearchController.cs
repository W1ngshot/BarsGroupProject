using Core.Services;
using Microsoft.AspNetCore.Mvc;
using WebApi.Dto;
using WebApi.Models;

namespace WebApi.Controllers;

public class SearchController : Controller
{
    private readonly ISearchService _searchService;
    private const int ElementsOnPage = 6;

    public SearchController(ISearchService searchService)
    {
        _searchService = searchService;
    }

    public async Task<SearchModel> Index(SearchDto searchDto)
    {
        var firstElementIndex = ElementsOnPage * (searchDto.PageNumber - 1);
        var searchModel = new SearchModel
        {
            SearchedArticles = await _searchService.FindArticlesAsync(searchDto.Text, searchDto.Tags,
                ElementsOnPage, firstElementIndex)
        };
        return searchModel;
    }
}