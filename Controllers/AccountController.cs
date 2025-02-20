using ConstructorApp.Models;
using ConstructorApp.Repository;
using ConstructorApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ConstructorApp.Controllers
{
    [AllowAnonymous]
    public class AccountController(IIdentityService identityService, ILoggingService logger) : BaseController(logger)
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
                // Veritabanından tam kullanıcı bilgilerini al
                var user = await identityService.GetUserByUsername(appUser.Email);

                // Tam kullanıcı bilgileriyle oturum aç
                await identityService.SignInAsync(user, false);
                logger.LogInfo("Giriş denemesi basarılı");
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "Kullanıcı adı veya şifre hatalı");
                logger.LogError("Giriş denemesi başarısz");
                return View(appUser);
            }

            // Bu satır hiç çalışmayacak, yukarıda her durumda return var
            // return RedirectToAction("Index", "Home");
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