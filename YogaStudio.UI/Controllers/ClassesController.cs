using Microsoft.AspNetCore.Mvc;
using YogaStudio.UI.Services;

namespace YogaStudio.UI.Controllers
{
    public class ClassesController : Controller
    {
        private readonly ApiService _apiService;

        public ClassesController(ApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<IActionResult> Index()
        {
            var lessons = await _apiService.GetLessonsAsync();
            return View(lessons);
        }
    }
}