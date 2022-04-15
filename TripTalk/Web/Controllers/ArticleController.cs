using Microsoft.AspNetCore.Mvc;
using Web.Dto;

namespace Web.Controllers;

public class ArticleController : Controller
{
    public IActionResult Index(int articleId)
    {
        //TODO сделать получение статьи и передавать модель статьи во view
        return View(articleId);
    }

    public IActionResult Create()
    {
        return View();
    }

    public IActionResult Create(CreateArticleDto article)
    {
        return View(article);
    }

    public IActionResult Edit(int articleId)
    {
        //TODO сделать получение статьи и передавать модель статьи во view
        return View(articleId);
    }
}