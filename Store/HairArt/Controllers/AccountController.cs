using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using HairArt.Models;

namespace HairArt.Controllers;

public class AccountController : Controller
{
    private readonly UserManager<IdentityUser> _userManager;

    private readonly SignInManager<IdentityUser> _signInManager;

    public AccountController(UserManager<IdentityUser> userManager,SignInManager<IdentityUser> signInManager)
    {
        _signInManager=signInManager;
        _userManager = userManager;
    }
    public IActionResult Login()
    {
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]

    public async Task<IActionResult> Login([FromForm] LoginModel model)
    {
        if(ModelState.IsValid)
        {
            IdentityUser user = await _userManager.FindByEmailAsync(model.Email);

            if(user is not null)
            {
                await _signInManager.SignOutAsync();
                if((await _signInManager.PasswordSignInAsync(user,model.Password,false,false)).Succeeded)
                {
                    return Redirect("/");
                }
            }

            ModelState.AddModelError("Error","Invalid email or password.");
        }
        return View();
    }

    public async Task<IActionResult> Logout(string ReturnUrl="/")
    {
        await _signInManager.SignOutAsync();
        return Redirect(ReturnUrl);
    }
}
