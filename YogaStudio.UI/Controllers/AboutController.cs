using Microsoft.AspNetCore.Mvc;
using YogaStudio.UI.Services;

namespace YogaStudio.UI.Controllers
{
    public class AboutController : Controller
    {
        private readonly ApiService _apiService;

        public AboutController(ApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<IActionResult> Index()
        {
            var trainers = await _apiService.GetTrainersAsync();
            return View(trainers);
        }
    }
}