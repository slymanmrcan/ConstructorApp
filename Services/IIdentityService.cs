using ConstructorApp.Models;
using Microsoft.AspNetCore.Identity;

namespace ConstructorApp.Services
{
    public interface IIdentityService
    {
        Task<bool> CheckUsernameExists(string username);
        Task<SignInResult> SignIn(string username, string password, bool rememberMe = false);
        Task<SignInResult> SignInAsync(AppUser user, bool rememberMe = false);
        Task SignOut();
        Task<IdentityResult> CreateUser(AppUser user, string password);
        Task<AppUser> GetUserById(string userId);
        Task<AppUser> GetUserByUsername(string username);
        Task<IdentityResult> UpdateUser(AppUser user);
        Task<IdentityResult> DeleteUser(string userId);
        Task<IList<string>> GetUserRoles(AppUser user);

        // Role işlemleri
        Task<bool> RoleExists(string roleName);
        Task<IdentityResult> CreateRole(string roleName);
        Task<IdentityResult> AddToRole(AppUser user, string roleName);
        Task<IdentityResult> RemoveFromRole(AppUser user, string roleName);
        Task<List<AppUser>> GetUsersInRole(string roleName);

    }
}