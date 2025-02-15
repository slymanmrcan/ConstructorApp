using ConstructorApp.Models;
using ConstructorApp.Repository;
using ConstructorApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ConstructorApp.Controllers
{
    [AllowAnonymous]
    public class AccountController(IIdentityService identityService) : BaseController
    {
        public async Task<IActionResult> Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(AppUser appUser)
        {
            if ((await identityService.SignIn(appUser.Email, appUser.PasswordHash, false)).Succeeded)
            {
                var user = await identityService.SignInAsync(appUser, false);
                
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "Kullanıc  adı  veya  şifre hatalı");
            }
            return RedirectToAction("Index", "Home");
        }
        public IActionResult Register()
        {
            return View();
        }

        public async Task<IActionResult> AddFirstUser()
        {
            await identityService.CreateUser(new AppUser
            {
                UserName = "admin",
                Email = "slymanmrcan@gmail.com",
                FirstName = "Admin",
                LastName = "Admin"
            }, "Admin123!");
            await identityService.CreateRole("Admin");
            identityService.AddToRole(await identityService.GetUserByUsername("admin"), "Admin").Wait();
            return RedirectToAction("Login");
        }
        public async Task<IActionResult> AddFirstRole()
        {
            await identityService.CreateRole("Personel");
            await identityService.CreateRole("Yönetici");
            return RedirectToAction("Login");
        }
        public async Task<IActionResult> Logout()
        {
            await identityService.SignOut();
            return RedirectToAction("Login", "Account");
        }
    }
}