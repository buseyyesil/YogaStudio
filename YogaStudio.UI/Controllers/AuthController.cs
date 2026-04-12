using Microsoft.AspNetCore.Mvc;
using YogaStudio.UI.Models;
using YogaStudio.UI.Services;

namespace YogaStudio.UI.Controllers
{
    public class AuthController : Controller
    {
        private readonly ApiService _apiService;

        public AuthController(ApiService apiService)
        {
            _apiService = apiService;
        }

        public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            var (success, message, userId, username, role) = await _apiService.LoginAsync(model);
            if (!success)
            {
                ViewBag.Error = message;
                return View(model);
            }

            HttpContext.Session.SetInt32("UserId", userId);
            HttpContext.Session.SetString("Username", username);
            HttpContext.Session.SetString("Role", role);

            if (role == "Admin")
                return RedirectToAction("Index", "Admin");

            return RedirectToAction("Index", "Default");
        }

        public IActionResult Register() => View();

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            var (success, message) = await _apiService.RegisterAsync(model);
            if (!success)
            {
                ViewBag.Error = message;
                return View(model);
            }

            TempData["Success"] = "Kayıt başarılı! Giriş yapabilirsiniz.";
            return RedirectToAction("Login");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}