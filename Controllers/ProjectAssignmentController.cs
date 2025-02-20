using System.Threading.Tasks;
using ConstructorApp.Models;
using ConstructorApp.Repository;
using ConstructorApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Models.ViewModels;

namespace ConstructorApp.Controllers
{
    public class ProjectAssignmentController(ILoggingService logger, IProjectAssignmentService projectAssignmentService,
     IIdentityService identityService, IProjectService projectService, IUnitOfWork unitOfWork) : BaseController(logger)
    {
        public async Task<IActionResult> Index(int page = 1)
        {
            int pageSize = 10;
            var (pagedData, totalPages) = await projectAssignmentService.GetPagedAsyncWithInclude(page, pageSize);
            var ProjectsasQueryable = await projectService.GetAllAsync();
            var projectViewModel = new ProjectAssignmentViewModel
            {
                PaginationModel = new PaginationModel
                {
                    CurrentPage = page,
                    TotalPages = totalPages,
                    PageUrl = "/ProjectAssignment/Index"
                },
                ProjectAssignment = pagedData,
                Projects = ProjectsasQueryable.ToList(),
                AppUsers = await identityService.GetAllUsers()
                // Önemli: Tüm müşteriler yerine sayfalanmış veriyi kullanıyoruz
            };
            return View(projectViewModel);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var allProject = await projectService.GetAllAsync();
            var data = allProject.Where(x => x.Status == "Pending"); // Önce sorguyu al
            ViewBag.ProjectSelectList = new SelectList(data, "Id", "Name");

            var allUsers = await identityService.GetAllUsers();
            // Önce sorguyu al
            ViewBag.UserSelectList = new SelectList(allUsers, "Id", "FullName");


            var viewModel = new CreateProjectAssignmentViewModel
            {
                Projects = allProject,
                AppUsers = await identityService.GetAllUsers(),
                ProjectAssignment = new ProjectAssignment()
            };

            return View(viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Create(int projectId, int userId)
        {
            if (projectId > 0 && userId > 0)  // Geçerli ID'ler için pozitif değer kontrolü
            {
                var project = await projectService.GetByIdAsync(projectId);

                var users = await identityService.GetUserById(userId.ToString());
                if (project != null || users != null)
                {

                    using var transaction = await unitOfWork.BeginTransactionAsync();
                    try
                    {
                        var projectAssignment = new ProjectAssignment
                        {
                            ProjectId = projectId,
                            AppUserId = userId  // Muhtemelen kolon adı 'AppUserId' olmalı, 'UserId' değil
                        };
                        await projectAssignmentService.AddAsync(projectAssignment);
                        project.Status = "InProgress";
                        await projectService.UpdateAsync(project);
                        await unitOfWork.SaveChangesAsync();
                        return RedirectToAction("Index"); // Başarılı durumda bir yönlendirme yapmalısınız
                    }

                    catch (Exception ex)
                    {
                        await transaction.RollbackAsync(); // Hata durumunda geri alma
                        logger.LogError("Create ProjectAssignment Error", ex);
                    }
                }
            }

            await PrepareViewForCreate();
            return View();
        }

        private async Task<IActionResult> PrepareViewForCreate()
        {
            var allProject = await projectService.GetAllAsync();
            var data = allProject.Where(x => x.Status == "Pending");
            ViewBag.ProjectSelectList = new SelectList(data, "Id", "Name");

            var allUsers = await identityService.GetAllUsers();
            ViewBag.UserSelectList = new SelectList(allUsers, "Id", "FullName");

            var viewModel = new CreateProjectAssignmentViewModel
            {
                Projects = allProject,
                AppUsers = await identityService.GetAllUsers(),
                ProjectAssignment = new ProjectAssignment()
            };

            return View(viewModel);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var projectAssignment = await projectAssignmentService.GetByIdAsync(id);

            if (projectAssignment != null)
            {
                await projectAssignmentService.DeleteAsync(projectAssignment.Id);
                await unitOfWork.SaveChangesAsync();

            }
            return RedirectToAction("Index");

        }

        public async Task<IActionResult> Details(int id)
        {
            var projectAssignment = await projectAssignmentService.GetByIdAsync(id);
            return View(projectAssignment);
        }
        public async Task<IActionResult> CompleteProject(int id)
        {
            var projectAssignment = await projectAssignmentService.GetByIdAsync(id);
            if (projectAssignment == null || projectAssignment.ProjectId == 0)
            {
                return RedirectToAction("Index");
            }

            var project = await projectService.GetByIdAsync(projectAssignment.ProjectId);
            if (project == null)
            {
                return RedirectToAction("Index");
            }

            project.Status = "Completed";
            await projectService.UpdateAsync(project);
            await unitOfWork.SaveChangesAsync();

            return RedirectToAction("Index");
        }
        // [HttpPost]
        // public async Task<IActionResult> Details(ProjectAssignment projectAssignment)
        // {
        //     var projectAssignment = await projectAssignmentService.GetByIdAsync(projectAssignment.Id);
        //     return View(projectAssignment);
        // }
    }
}