using ConstructorApp.Models;
using ConstructorApp.Repository;
using ConstructorApp.Services;
using Microsoft.AspNetCore.Mvc;
using Models.ViewModels;

namespace ConstructorApp.Controllers
{
    public class ProjectController(IProjectService projectService,IUnitOfWork unitOfWork) : BaseController
    {
        public async Task<IActionResult> IndexAsync(int page = 1)
        {
            int pageSize = 10;
            var (pagedData, totalPages) = await projectService.GetPagedAsync(page, pageSize);
            var projectViewModel = new ProjectViewModel
            {
                PaginationModel = new PaginationModel
                {
                    CurrentPage = page,
                    TotalPages = totalPages,
                    PageUrl = "/Customer/Index"
                },
                Project = pagedData  // Önemli: Tüm müşteriler yerine sayfalanmış veriyi kullanıyoruz
            };
            return View(projectViewModel);
        }


        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Project project)
        {
            if (ModelState.IsValid)
            {
                projectService.AddAsync(project);
                await unitOfWork.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(project);
        }
    }
}