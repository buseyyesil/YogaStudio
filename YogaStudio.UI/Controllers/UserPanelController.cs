using Microsoft.AspNetCore.Mvc;
using YogaStudio.UI.Services;

namespace YogaStudio.UI.Controllers
{
    public class UserPanelController : Controller
    {
        private readonly ApiService _apiService;

        public UserPanelController(ApiService apiService)
        {
            _apiService = apiService;
        }

        private IActionResult? CheckLogin()
        {
            if (HttpContext.Session.GetInt32("UserId") == null)
            {
                TempData["Error"] = "Bu sayfaya erişmek için giriş yapmalısınız.";
                return RedirectToAction("Login", "Auth");
            }
            return null;
        }

        public async Task<IActionResult> Index()
        {
            var check = CheckLogin();
            if (check != null) return check;

            var userId = HttpContext.Session.GetInt32("UserId")!.Value;
            var reservations = await _apiService.GetUserReservationsAsync(userId);
            var userPackages = await _apiService.GetUserPackagesAsync(userId);

            ViewBag.Username = HttpContext.Session.GetString("Username");
            ViewBag.Role = HttpContext.Session.GetString("Role");
            ViewBag.UserPackages = userPackages;

            return View(reservations);
        }

        public async Task<IActionResult> Packages()
        {
            var check = CheckLogin();
            if (check != null) return check;

            var packages = await _apiService.GetPackagesAsync();
            return View(packages);
        }

        [HttpPost]
        public async Task<IActionResult> PurchasePackage(int packageId)
        {
            var check = CheckLogin();
            if (check != null) return check;

            var userId = HttpContext.Session.GetInt32("UserId")!.Value;
            var success = await _apiService.PurchasePackageAsync(userId, packageId);

            TempData[success ? "Success" : "Error"] = success
                ? "Paket başarıyla satın alındı! 🎉"
                : "Paket satın alınamadı.";

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> CancelReservation(int id)
        {
            var check = CheckLogin();
            if (check != null) return check;

            var success = await _apiService.DeleteReservationAsync(id);
            TempData[success ? "Success" : "Error"] = success
                ? "Rezervasyon iptal edildi."
                : "Rezervasyon iptal edilemedi.";

            return RedirectToAction("Index");
        }
    }
}