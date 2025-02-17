using System.Security.Principal;
using System.Threading.Tasks;
using ConstructorApp.Models;
using ConstructorApp.Repository;
using ConstructorApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models.ViewModels;

namespace ConstructorApp.Controllers
{
    [Authorize(Roles = "Yönetici")]
    public class UserController(IIdentityService identityService, IUnitOfWork unitOfWork,ILoggingService logger) : BaseController(logger)
    {
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Index(int page = 1)
        {
            int pageSize = 10;
            var (pagedData, totalPages) = await identityService.GetPagedAsync(page, pageSize);
            var userViewModel = new UserViewModel
            {
                PaginationModel = new PaginationModel
                {
                    CurrentPage = page,
                    TotalPages = totalPages,
                    PageUrl = "/User/Index"
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
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var user = await identityService.GetUserById(id.ToString());
            var userRole = await identityService.GetUserRoles(user);

            var roles = await identityService.GetAllRoles();
            ViewBag.roles = new SelectList(roles, "Name", "Name", userRole.FirstOrDefault());
            return View(user);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Details(AppUser user, string? password)
        {
            var roles = await identityService.GetAllRoles();
            var userRole = await identityService.GetUserRoles(user);
            ViewBag.roles = new SelectList(roles, "Name", "Name", userRole.FirstOrDefault());

            var userDetail = await identityService.GetUserById(user.Id.ToString());
            if (userDetail == null) return View(user);

            if (ModelState.IsValid)
            {
                userDetail.Email = user.Email;
                userDetail.UserName = user.Email;
                userDetail.NormalizedEmail = user.Email?.ToUpper();
                userDetail.NormalizedUserName = user.Email?.ToUpper();
                userDetail.FirstName = user.FirstName;
                userDetail.LastName = user.LastName;
                userDetail.PhoneNumber = user.PhoneNumber;
                userDetail.ModifiedDate = DateTime.Now;
                if (password != null)
                {
                    await identityService.UpdatePassword(userDetail, password);
                    await unitOfWork.SaveChangesAsync();
                }

                userDetail.SecurityStamp = Guid.NewGuid().ToString();
                var updateUser = await identityService.UpdateUser(userDetail);
                // if (updateUser.Succeeded)
                // {
                //     await identityService.UpdateSecurityStamp(user);
                // }
                await unitOfWork.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Güncelleme başarısız!");
                return View(user);
            }
        }
        public async Task<IActionResult> Delete(int id)
        {
            await identityService.DeleteUser(id.ToString());
            await unitOfWork.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}