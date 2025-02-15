using ConstructorApp.Models;
using Microsoft.AspNetCore.Identity;

namespace ConstructorApp.Services
{
    public class IdentityService(UserManager<AppUser> _userManager, SignInManager<AppUser> _signInManager, RoleManager<AppRole> _roleManager) : IIdentityService
    {
        public async Task<IdentityResult> AddToRole(AppUser user, string roleName)
        {
            return await _userManager.AddToRoleAsync(user, roleName);
        }
        public async Task<bool> CheckUsernameExists(string username)
        {
            return await _userManager.FindByNameAsync(username) != null;
        }
        public async Task<IdentityResult> CreateUser(AppUser user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }
        public async Task<IdentityResult> DeleteUser(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                return await _userManager.DeleteAsync(user);
            }
            return IdentityResult.Failed(new IdentityError { Description = "Kullanıcı bulunamadı" });

        }
        public async Task<AppUser> GetUserById(string userId)
        {
            return await _userManager.FindByIdAsync(userId);
        }
        public async Task<IdentityResult> CreateRole(string roleName)
        {
            if (!await RoleExists(roleName))
            {
                return await _roleManager.CreateAsync(new AppRole { Name = roleName });
            }
            return IdentityResult.Failed(new IdentityError { Description = "Rol zaten mevcut" });

        }

        public async Task<AppUser> GetUserByUsername(string username)
        {
            return await _userManager.FindByNameAsync(username);
        }

        public async Task<IList<string>> GetUserRoles(AppUser user)
        {
             return await _userManager.GetRolesAsync(user);
        }

        public async Task<List<AppUser>> GetUsersInRole(string roleName)
        {
            return (await _userManager.GetUsersInRoleAsync(roleName)).ToList();
        }

        public async Task<IdentityResult> RemoveFromRole(AppUser user, string roleName)
        {
            return await _userManager.RemoveFromRoleAsync(user, roleName);
        }

        public async Task<bool> RoleExists(string roleName)
        {
            return await _roleManager.RoleExistsAsync(roleName);
        }

        public async Task<SignInResult> SignIn(string username, string password, bool rememberMe = false)
        {
            var result = await _signInManager.PasswordSignInAsync(
            username,
            password,
            rememberMe,
            lockoutOnFailure: false);

            return result;
        }

        public async Task SignOut()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<IdentityResult> UpdateUser(AppUser user)
        {
            return await _userManager.UpdateAsync(user);
        }

        public async Task<SignInResult> SignInAsync(AppUser user,bool rememberMe = false)
        {
            await _signInManager.SignInAsync(user, rememberMe);
            return SignInResult.Success;
        }
    }
}