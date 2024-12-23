using Microsoft.AspNetCore.Mvc;
namespace HairArt.Controllers;

public class AccountController : Controller
{
    public IActionResult Login()
    {
        return View();
    }
}
