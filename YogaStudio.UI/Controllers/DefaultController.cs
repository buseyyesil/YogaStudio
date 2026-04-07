using Microsoft.AspNetCore.Mvc;

namespace YogaStudio.UI.Controllers
{
    public class DefaultController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
