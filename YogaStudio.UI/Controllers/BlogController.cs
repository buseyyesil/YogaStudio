using Microsoft.AspNetCore.Mvc;

namespace YogaStudio.UI.Controllers
{
    public class BlogController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}