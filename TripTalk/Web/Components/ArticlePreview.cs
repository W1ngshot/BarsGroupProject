using Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace OldWeb.Components;

public class ArticlePreview : ViewComponent
{
    public IViewComponentResult Invoke(Article? article)
    {
        ViewBag.Id = article.Id;
        ViewBag.Title = article.Title;
        ViewBag.ShortDescription = article.ShortDescription ?? "";
        ViewBag.Views = article.Views;
        ViewBag.Rating = article.Rating;
        ViewBag.Tags = article.TagNames;
        ViewBag.UserNickname = article.UserNickname;
        if (article.PreviewPictureLink != null) 
            ViewBag.PreviewPictureLink = article.PreviewPictureLink;
        return View("~/Views/ViewComponents/ArticlePreview.cshtml");
    }
}