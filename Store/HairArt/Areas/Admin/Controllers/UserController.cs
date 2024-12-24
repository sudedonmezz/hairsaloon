using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using Entities.Models;
namespace HairArt.Areas.Admin.Controllers;

[Area("Admin")]
public class UserController : Controller
{
    private readonly IServiceManager _manager;

    public UserController(IServiceManager manager)
    {
        _manager = manager;
    }

    public IActionResult Index()
    {
        var users=_manager.AuthService.GetAllUsers();
        return View(users);
    }
}
