using Core;
using Core.Services;
using Microsoft.AspNetCore.Mvc;
using Web.Dto;
using Web.Models;

namespace Web.Controllers;

public class MainController : Controller
{
    private readonly IArticleService _articleService;

    public MainController(IArticleService articleService)
    {
        _articleService = articleService;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var popularArticles = await _articleService.GetCategoryArticlesAsync(Category.Popular, Period.AllTime, 4);
        var mainArticleModel = new MainPageArticlesModel
        {
            PopularArticles = popularArticles
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