using System.Security.Principal;
using System.Threading.Tasks;
using ConstructorApp.Models;
using ConstructorApp.Repository;
using ConstructorApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models.ViewModels;

namespace ConstructorApp.Controllers
{
    public class UserController(IIdentityService identityService, IUnitOfWork unitOfWork) : BaseController
    {
        public async Task<IActionResult> IndexAsync(int page = 1)
        {
            int pageSize = 10;
            var (pagedData, totalPages) = await identityService.GetPagedAsync(page, pageSize);
            var userViewModel = new UserViewModel
            {
                PaginationModel = new PaginationModel
                {
                    CurrentPage = page,
                    TotalPages = totalPages,
                    PageUrl = "/Project/Index"
                },
                AppUser = pagedData  // Önemli: Tüm müşteriler yerine sayfalanmış veriyi kullanıyoruz
            };
            return View(userViewModel);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var roles = await identityService.GetAllRoles();
            ViewBag.roles = new SelectList(roles, "Name", "Name");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AppUser user, string password, string roleName)
        {
            var roles = await identityService.GetAllRoles();
            ViewBag.roles = new SelectList(roles, "Name", "Name");
            if (ModelState.IsValid)
            {
                // Kullanıcıyı oluştur
                user.UserName = user.Email;
                var result = await identityService.CreateUser(user, password);
                await unitOfWork.SaveChangesAsync();
                if (result.Succeeded)
                {
                    // Kullanıcı oluşturuldu, ID'yi alabiliriz
                    var userId = user.Id; // ID'yi doğrudan al

                    // Rol atamasını yap
                    await identityService.AddToRole(user, roleName);

                    // Değişiklikleri kaydet
                    await unitOfWork.SaveChangesAsync();

                    return RedirectToAction("Index");
                }
                else
                {
                    // Hata durumu, result hatalarını işle
                    ModelState.AddModelError(string.Empty, "Kullanıcı oluşturulurken bir hata oluştu.");
                }
            }
            return View();
        }

        public async Task<IActionResult> Details(int id)
        {
            var user = await identityService.GetUserById(id.ToString());
            var roles = await identityService.GetAllRoles();
            ViewBag.roles = new SelectList(roles, "Name", "Name");
            return View(user);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Details(AppUser user)
        {
            var roles = await identityService.GetAllRoles();
            ViewBag.roles = new SelectList(roles, "Name", "Name");
            if (ModelState.IsValid)
            {
                await identityService.UpdateUser(user);
                await unitOfWork.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(user);
        }
        public async Task<IActionResult> Delete(int id)
        {
            await identityService.DeleteUser(id.ToString());
            await unitOfWork.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}