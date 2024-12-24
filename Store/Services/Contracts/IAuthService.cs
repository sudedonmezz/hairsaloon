using Microsoft.AspNetCore.Identity;
using Entities.Models;
namespace Services.Contracts;

    public interface IAuthService
    {
        IEnumerable<IdentityRole> Roles { get; }
        IEnumerable<ApplicationUser> GetAllUsers();


    }

