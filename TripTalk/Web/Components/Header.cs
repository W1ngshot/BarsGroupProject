using Microsoft.AspNetCore.Mvc;

namespace OldWeb.Components;

public class Header : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        return View("~/Views/ViewComponents/Header.cshtml");
    }
}