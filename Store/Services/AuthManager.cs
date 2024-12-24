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

    public async Task<ApplicationUser> GetOneUser(string email)
{
    var user = await _userManager.FindByEmailAsync(email);
    if (user == null)
    {
        throw new Exception($"User with email '{email}' not found.");
    }
    return user;
}



  public async Task<UserDtoForUpdate> GetOneUserForUpdate(string email)
{
    var user = await GetOneUser(email);
    if (user == null)
    {
        Console.WriteLine($"User not found with email: {email}");
        throw new Exception($"User with email '{email}' not found.");
    }

    var userDto = _mapper.Map<UserDtoForUpdate>(user);
    Console.WriteLine($"UserDto successfully mapped for email: {email}");

    userDto.Roles = new HashSet<string>(_roleManager.Roles.Select(r => r.Name).ToList());
    userDto.UserRoles = new HashSet<string>(await _userManager.GetRolesAsync(user));

    return userDto;
}




 public async Task Update(UserDtoForUpdate userDto)
{
    Console.WriteLine($"Starting update for user: {userDto.Email}");

    var user = await _userManager.FindByEmailAsync(userDto.Email);
    if (user is not null)
    {
        Console.WriteLine($"User found: {user.Email}");

        // Güncelleme işlemi
        user.PhoneNumber = userDto.PhoneNumber;
        user.Email = userDto.Email;

        var result = await _userManager.UpdateAsync(user);
        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
            {
                Console.WriteLine($"Update error: {error.Description}");
            }
            throw new Exception("User update failed.");
        }

        if (userDto.Roles.Count > 0)
        {
            var currentRoles = await _userManager.GetRolesAsync(user);
            Console.WriteLine($"Current roles: {string.Join(", ", currentRoles)}");

            var removeResult = await _userManager.RemoveFromRolesAsync(user, currentRoles);
            if (!removeResult.Succeeded)
            {
                throw new Exception("Error removing roles.");
            }

            var addResult = await _userManager.AddToRolesAsync(user, userDto.Roles);
            if (!addResult.Succeeded)
            {
                throw new Exception("Error adding roles.");
            }
            Console.WriteLine($"Updated roles: {string.Join(", ", userDto.Roles)}");
        }

        Console.WriteLine($"Update completed successfully for user: {userDto.Email}");
        return;
    }

    throw new Exception($"User with email '{userDto.Email}' not found.");
}


public async Task<IEnumerable<string>> GetOneUserRoles(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                throw new Exception($"'{email}' adresli kullanıcı bulunamadı.");
            }

            return await _userManager.GetRolesAsync(user);
        }


}