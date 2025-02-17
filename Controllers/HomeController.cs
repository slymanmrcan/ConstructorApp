using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ConstructorApp.Models;
using ConstructorApp.Services;
using System.Threading.Tasks;
using Models.ChartViewModels;

namespace ConstructorApp.Controllers;

public class HomeController(IProjectService projectService,ILoggingService logger) : BaseController(logger)
{
    public async Task<IActionResult> Index()
    {
        var completed =await projectService.GetCountFinished();
        var inprogress =await projectService.GetCountProgress();
        var pending =await projectService.GetCountPending();
        ViewBag.completed =completed;
        ViewBag.inprogress =inprogress;
        ViewBag.pending =pending;
        ViewBag.total =completed+inprogress+pending;
        ViewBag.totalprize=await projectService.GetAllPrize();
        var prize =await projectService.GetPrize();
        var project = await projectService.GetAllProjects();
        ChartCombineModel model = new ChartCombineModel{
            AllProject = project,
            Top3Project = prize
        };
        var modelList = new List<ChartCombineModel> { model };
        return View(modelList);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
