using Microsoft.AspNetCore.Identity;
using Entities.Models;
using System.Collections.Generic;

using Services.Contracts;
namespace Services;

public class AuthManager :IAuthService
{
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<ApplicationUser> _userManager;

    public AuthManager(RoleManager<IdentityRole> roleManager,UserManager<ApplicationUser> userManager)
    {
        _userManager=userManager;
        _roleManager = roleManager;
    }
    public IEnumerable<IdentityRole> Roles=>
     _roleManager.Roles;

     public IEnumerable<ApplicationUser> GetAllUsers()
     {
        return _userManager.Users.ToList();
     }
}
