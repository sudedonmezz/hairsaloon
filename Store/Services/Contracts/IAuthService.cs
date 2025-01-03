using Microsoft.AspNetCore.Identity;
using Entities.Models;
namespace Services.Contracts;

    public interface IAuthService
    {
        IEnumerable<IdentityRole> Roles { get; }
        IEnumerable<ApplicationUser> GetAllUsers();

        Task<IdentityResult>CreateUser(UserDtoForCreation userDto);
        Task<UserDtoForUpdate> GetOneUserForUpdate(string email);
        Task<ApplicationUser> GetOneUser(string userName);

        Task Update(UserDtoForUpdate userDto);

         Task<IEnumerable<string>> GetOneUserRoles(string email);

         Task<IdentityResult> ResetPassword(ResetPasswordDto model);

          Task<IdentityResult> DeleteOneUser(string email);

        Task<bool> HasAppointments(string userId);

        Task<IdentityResult> CreateUser(ApplicationUser user, string password);

       
       Task<IdentityResult> AddToRole(ApplicationUser user, string role);

    }

