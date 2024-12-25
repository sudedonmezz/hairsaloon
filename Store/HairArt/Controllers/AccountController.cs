using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Entities.Models;
using HairArt.Models;

namespace HairArt.Controllers;

public class AccountController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;

    private readonly SignInManager<ApplicationUser> _signInManager;

    public AccountController(UserManager<ApplicationUser> userManager,SignInManager<ApplicationUser> signInManager)
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
            ApplicationUser user = await _userManager.FindByEmailAsync(model.Email);


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

    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register([FromForm]RegisterDto model)
    {
        var user=new ApplicationUser
        {
            Name=model.Name,
            LastName=model.LastName,
            Email=model.Email,
            UserName = model.Email,
            PhoneNumber = model.PhoneNumber
           
        };
        var result=await _userManager.CreateAsync(user,model.Password);

        if(result.Succeeded)
        {
            var roleResult=await _userManager 
            .AddToRoleAsync(user,"User");
            if(roleResult.Succeeded)
            {
                return RedirectToAction("Login");
            }
        }
        else 
        {
            foreach (var err in result.Errors)
            {
                ModelState.AddModelError("",err.Description);
            }
        }
        return View();
    }

    public IActionResult AccessDenied([FromQuery(Name="ReturnUrl")]string returnUrl)
    {
        return View();
    }
    
}
