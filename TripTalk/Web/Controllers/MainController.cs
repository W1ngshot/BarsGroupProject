using Microsoft.AspNetCore.Mvc;
using Web.Dto;

namespace Web.Controllers;

public class MainController : Controller
{

    [HttpGet]
    public IActionResult Index()
    {
        return View();
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