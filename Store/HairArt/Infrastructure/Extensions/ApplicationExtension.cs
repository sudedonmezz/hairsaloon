using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using HairArt.Infrastructure.Extensions;


using Repositories;

namespace HairArt.Infrastructure.Extensions
{
    public static class ApplicationExtension
    {
     public static async void ConfigureDefaultAdminUser(this IApplicationBuilder app)
{
    const string adminUser="Admin";
    const string adminPassword="Admin+123456";

    //usermanager
    UserManager<IdentityUser> userManager=app
    .ApplicationServices
    .CreateScope()
    .ServiceProvider
    .GetRequiredService<UserManager<IdentityUser>>();
    
    //rolemanager
    RoleManager<IdentityRole> roleManager=app
    .ApplicationServices
    .CreateAsyncScope()
    .ServiceProvider
    .GetRequiredService<RoleManager<IdentityRole>>();

    IdentityUser user = await userManager.FindByNameAsync(adminUser);

    if(user is null)
    {
        user=new IdentityUser(adminUser)
        {
            Email="b221210581@ogr.sakarya.edu.tr",
            PhoneNumber="5061112233",
            UserName=adminUser,
            
        

        };

        var result= await userManager.CreateAsync(user,adminPassword);

        if(!result.Succeeded)
        {
            throw new Exception("Admin user could not created");
        }

        var roleResult=await userManager.AddToRolesAsync(user,
      roleManager
      .Roles
      .Select(r=>r.Name)
      .ToList()
        );

        if(!roleResult.Succeeded)
        throw new Exception("System have problems with role defination for admin");
    }
}
    }
}
