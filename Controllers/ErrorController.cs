using ConstructorApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ConstructorApp.Controllers
{
    [AllowAnonymous]
    public class ErrorController(ILoggingService logger) : BaseController(logger)
    {
        public async Task<IActionResult> All(int? statusCode)
        {
            if (statusCode != null)
            {
                if (statusCode == 403)
                {
                    return View("403");

                }
                else if (statusCode == 404)
                {
                    return View("404");

                }
            }
            return View("Global");
        }
        public IActionResult AccessDenied()
        {
            return View("403");
        }
    }
}