using Microsoft.AspNetCore.Mvc;

namespace YogaStudio.UI.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
