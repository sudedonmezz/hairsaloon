using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using HairArt.Infrastructure.Extensions;
using Entities.Models;
using Repositories;

namespace HairArt.Infrastructure.Extensions
{
    public static class ApplicationExtension
    {
       public static async void ConfigureDefaultAdminUser(this IApplicationBuilder app)
{
    const string adminUser = "Admin";
    const string adminPassword = "Admin+123456";

    // UserManager
    UserManager<ApplicationUser> userManager = app
        .ApplicationServices
        .CreateScope()
        .ServiceProvider
        .GetRequiredService<UserManager<ApplicationUser>>();

    // RoleManager
    RoleManager<IdentityRole> roleManager = app
        .ApplicationServices
        .CreateAsyncScope()
        .ServiceProvider
        .GetRequiredService<RoleManager<IdentityRole>>();

    ApplicationUser user = await userManager.FindByNameAsync(adminUser);

    if (user == null)
    {
        // Yeni kullanıcı oluştur
        user = new ApplicationUser
        {
            UserName = adminUser,
            Email = "b221210581@ogr.sakarya.edu.tr",
            PhoneNumber = "5061112233",
            Name = "Sude",
            LastName = "Donmez"
        };

        var result = await userManager.CreateAsync(user, adminPassword);

        if (!result.Succeeded)
        {
            throw new Exception("Admin user could not be created.");
        }

        var roleResult = await userManager.AddToRolesAsync(user,
            roleManager
                .Roles
                .Select(r => r.Name)
                .ToList()
        );

        if (!roleResult.Succeeded)
        {
            throw new Exception("System has problems with role definition for admin.");
        }
    }
    else
    {
        // Mevcut kullanıcıyı güncelle
        // Mevcut kullanıcıyı güncelle
       
        user.Name = "Sude";
        user.LastName = "Donmez";

        var updateResult = await userManager.UpdateAsync(user);

        if (!updateResult.Succeeded)
        {
            foreach (var error in updateResult.Errors)
            {
                Console.WriteLine($"Update Error: {error.Description}");
            }
            throw new Exception("Failed to update admin user.");
        }
    }
}

    }
}
