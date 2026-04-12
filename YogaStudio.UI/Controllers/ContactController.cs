using Microsoft.AspNetCore.Mvc;
using YogaStudio.UI.Services;

namespace YogaStudio.UI.Controllers
{
    public class ContactController : Controller
    {
        private readonly ApiService _apiService;

        public ContactController(ApiService apiService)
        {
            _apiService = apiService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage(string Name, string Email, string Phone, string Subject, string Message)
        {
            if (string.IsNullOrWhiteSpace(Name) || string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Message))
            {
                TempData["Error"] = "Ad, email ve mesaj boş olamaz.";
                return RedirectToAction("Index");
            }

            var success = await _apiService.SendContactMessageAsync(Name, Email, Phone ?? "", Subject ?? "", Message);
            TempData[success ? "Success" : "Error"] = success
                ? "Mesajınız başarıyla gönderildi! En kısa sürede dönüş yapacağız. 🎉"
                : "Mesaj gönderilemedi. Lütfen tekrar deneyin.";

            return RedirectToAction("Index");
        }
    }
}