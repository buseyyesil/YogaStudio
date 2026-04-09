using Microsoft.AspNetCore.Mvc;
using YogaStudio.UI.Services;

namespace YogaStudio.UI.Controllers
{
    public class BlogController : Controller
    {
        private readonly ApiService _apiService;

        public BlogController(ApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<IActionResult> Index()
        {
            var blogs = await _apiService.GetBlogsAsync();
            return View(blogs);
        }
    }
}