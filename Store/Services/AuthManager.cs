using Microsoft.AspNetCore.Identity;
using Entities.Models;
using System.Collections.Generic;
using AutoMapper;

using Services.Contracts;

namespace Services;

public class AuthManager :IAuthService
{
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<ApplicationUser> _userManager;

    private readonly IMapper _mapper;

    public AuthManager(RoleManager<IdentityRole> roleManager,UserManager<ApplicationUser> userManager,IMapper mapper)
    {
        _mapper=mapper;
        _userManager=userManager;
        _roleManager = roleManager;
    }
    public IEnumerable<IdentityRole> Roles=>
     _roleManager.Roles;

     public IEnumerable<ApplicationUser> GetAllUsers()
     {
        return _userManager.Users.ToList();
     }

     public async Task<IdentityResult> CreateUser(UserDtoForCreation userDto)
     {
        var user=_mapper.Map<ApplicationUser>(userDto);
        var result=await _userManager.CreateAsync(user,userDto.Password);
        if(!result.Succeeded)
        {
            throw new Exception("user could not be created");
        }

        if(userDto.Roles.Count>0)
        {
            var roleResult=await _userManager.AddToRolesAsync(user,userDto.Roles);
            if(!roleResult.Succeeded)
            throw new Exception("System have problems with roles.");
        }
        return result;
     }
}
