// filepath: /d:/All Code/kmitl/y2_s2/web_app/project/HikeHub/Controllers/HomeController.cs
using Microsoft.AspNetCore.Mvc;

namespace HikeHub.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return Content("Hello World");
        }
    }
}