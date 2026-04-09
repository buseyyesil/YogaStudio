using Microsoft.AspNetCore.Mvc;
using YogaStudio.UI.Models;
using YogaStudio.UI.Services;

namespace YogaStudio.UI.Controllers
{
    public class DefaultController : Controller
    {
        private readonly ApiService _apiService;

        public DefaultController(ApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<IActionResult> Index()
        {
            var lessons = await _apiService.GetLessonsAsync();
            var packages = await _apiService.GetPackagesAsync();
            var blogs = await _apiService.GetBlogsAsync();
            var testimonies = await _apiService.GetTestimoniesAsync();

            var model = new HomeViewModel
            {
                Lessons = lessons,
                Packages = packages,
                Blogs = blogs,
                Testimonies = testimonies
            };

            return View(model);
        }
    }
}