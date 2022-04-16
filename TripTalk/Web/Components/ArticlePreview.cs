using Microsoft.AspNetCore.Mvc;

namespace Web.Components;

public class ArticlePreview : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        return View("~/ViewComponents/ArticlePreview.cshtml");
    }
}