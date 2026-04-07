using Microsoft.AspNetCore.Mvc;
using YogaStudio.UI.Models;
using YogaStudio.UI.Services;

namespace YogaStudio.UI.Controllers
{
    public class ScheduleController : Controller
    {
        private readonly ApiService _apiService;

        public ScheduleController(ApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<IActionResult> Index()
        {
            var lessons = await _apiService.GetLessonsAsync();
            var grouped = lessons
                .GroupBy(x => x.Date.DayOfWeek)
                .ToDictionary(g => g.Key, g => g.ToList());
            return View(grouped);
        }

        [HttpPost]
        public async Task<IActionResult> Reserve(int lessonId)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                TempData["Error"] = "Rezervasyon için giriş yapmalısınız.";
                return RedirectToAction("Index");
            }

            var model = new CreateReservationViewModel
            {
                UserId = userId.Value,
                LessonId = lessonId
            };

            var success = await _apiService.CreateReservationAsync(model);
            TempData[success ? "Success" : "Error"] = success
                ? "Rezervasyon başarıyla oluşturuldu!"
                : "Rezervasyon oluşturulamadı. Kontenjan dolu olabilir.";

            return RedirectToAction("Index");
        }
    }
}