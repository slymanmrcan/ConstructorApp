using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ConstructorApp.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {

    }
}