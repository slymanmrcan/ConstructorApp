using Microsoft.AspNetCore.Mvc;

namespace ConstructorApp.Controllers
{
    public class LoginController : BaseController
    {
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }
    }
}