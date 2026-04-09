using Microsoft.AspNetCore.Mvc;

namespace YogaStudio.UI.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SendMessage(string Name, string Email, string Subject, string Message)
        {
            if (string.IsNullOrWhiteSpace(Name) || string.IsNullOrWhiteSpace(Email))
            {
                TempData["Error"] = "Ad ve email boş olamaz.";
                return RedirectToAction("Index");
            }

            TempData["Success"] = "Mesajınız başarıyla gönderildi. En kısa sürede dönüş yapacağız!";
            return RedirectToAction("Index");
        }
    }
}