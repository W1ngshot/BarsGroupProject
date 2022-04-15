using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Dto;

namespace Web.Controllers;

[Authorize]
public class AccountController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult MyArticles()
    {
        return View();
    }


    public IActionResult Logout()
    {
        return RedirectToAction("Logout", "Auth");
    }

    public IActionResult ChangePassword()
    {
        return View();
    }

    //TODO реализовать
    public IActionResult ChangePassword(ChangePasswordDto changePasswordModel)
    {
        return RedirectToAction("Index");
    }
}