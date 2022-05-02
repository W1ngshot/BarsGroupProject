using Core;
using Core.Services;
using Microsoft.AspNetCore.Mvc;
using OldWeb.Dto;
using OldWeb.Models;

namespace OldWeb.Controllers;

public class MainController : Controller
{
    private readonly IArticleCategoryService _articleCategoryService;

    public MainController(IArticleCategoryService articleCategoryService)
    {
        _articleCategoryService = articleCategoryService;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var popularArticles = await _articleCategoryService.GetOrderedArticlesAsync(Category.Popular, Period.AllTime, 4);
        var latestArticles = await _articleCategoryService.GetOrderedArticlesAsync(Category.Last, Period.AllTime, 4);
        var bestArticles = await _articleCategoryService.GetOrderedArticlesAsync(Category.Best, Period.AllTime, 4);
        var mainArticleModel = new MainPageArticlesModel
        {
            PopularArticles = popularArticles,
            LatestArticles = latestArticles,
            BestArticles = bestArticles
        };
        return View(mainArticleModel);
    }

    public IActionResult Index(SearchDto searchModel)
    {
        return RedirectToAction("Search", searchModel);
    }

    [HttpGet]
    public IActionResult Search()
    {
        return View();
    }

    public IActionResult Search(SearchDto searchModel)
    {
        //TODO реализовать поиск
        return View(searchModel);
    }
}