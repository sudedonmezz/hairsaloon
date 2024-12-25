using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
namespace HairArt.Areas.Admin.Controllers;




[Area("Admin")]
[Authorize(Roles = "Admin")]
public class CategoryController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
