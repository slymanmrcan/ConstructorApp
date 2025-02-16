using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ConstructorApp.Models;
using ConstructorApp.Services;
using System.Threading.Tasks;

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
        return View();
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
