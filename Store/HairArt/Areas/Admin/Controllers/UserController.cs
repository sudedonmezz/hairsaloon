using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
namespace HairArt.Areas.Admin.Controllers;


[Area("Admin")]
[Authorize(Roles = "Admin")]
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
    public IActionResult Create()
    {
        return View(new UserDtoForCreation()
        {
            Roles=new HashSet<string>(_manager.AuthService.Roles.Select(x => x.Name)
            .ToList())
        });

    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([FromForm]UserDtoForCreation userDto)
    {
        var result= await _manager.AuthService.CreateUser(userDto);
        return result.Succeeded
        ? RedirectToAction("Index")
        :View();
        return View();
    }


public async Task<IActionResult> Update([FromRoute(Name = "id")] string id)
{
    try
    {
        var user = await _manager.AuthService.GetOneUserForUpdate(id);
        if (user == null)
        {
            TempData["ErrorMessage"] = $"User with email '{id}' not found.";
            return RedirectToAction("Index");
        }
        return View(user);
    }
    catch (Exception ex)
    {
        TempData["ErrorMessage"] = ex.Message;
        return RedirectToAction("Index");
    }
}




  [HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Update([FromForm] UserDtoForUpdate userDto)
{
    if (ModelState.IsValid)
    {
        try
        {
            // Güncelleme işlemi
            await _manager.AuthService.Update(userDto);

            // Başarılı güncelleme
            Console.WriteLine("Update successful for user: " + userDto.Email);
            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            // Hata yakala ve logla
            Console.WriteLine("Error during update: " + ex.Message);
            ModelState.AddModelError(string.Empty, ex.Message);
        }
    }
    else
    {
        // ModelState hatalarını logla
        foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
        {
            Console.WriteLine("Validation error: " + error.ErrorMessage);
        }
    }

    // Rolleri tekrar yükle
    userDto.Roles = new HashSet<string>(_manager.AuthService.Roles.Select(r => r.Name).ToList());
    userDto.UserRoles = new HashSet<string>(await _manager.AuthService.GetOneUserRoles(userDto.Email));

    return View(userDto); // Hatalıysa aynı sayfaya dön
}
public async Task<IActionResult> ResetPassword([FromRoute(Name="id")]string id)
{
return View(new ResetPasswordDto()
{
    Email=id
});
}
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ResetPassword([FromForm]ResetPasswordDto model)
    {
    var result= await _manager.AuthService.ResetPassword(model);
    return result.Succeeded
    ? RedirectToAction("Index")
    : View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> DeleteOneUser([FromForm]UserDto userDto)
    {
        var result = await _manager
        .AuthService
        .DeleteOneUser(userDto.Email);

        return result.Succeeded
        ? RedirectToAction("Index")
        : View();
    }

}