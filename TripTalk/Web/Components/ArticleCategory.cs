using Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Web.Components;

public class ArticleCategory : ViewComponent
{
    public IViewComponentResult Invoke(string categoryName, List<Article> articles)
    {
        ViewBag.CategoryName = categoryName;
        ViewBag.Articles = articles;
        return View("~/Views/ViewComponents/ArticleCategory.cshtml");
    }
}