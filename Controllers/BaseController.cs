using ConstructorApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ConstructorApp.Controllers
{
    [Authorize]
    public class BaseController(ILoggingService logger) : Controller
    {

    }
}