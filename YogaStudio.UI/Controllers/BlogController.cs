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
        public async Task<IActionResult> Detail(int id)
        {
            var blog = await _apiService.GetBlogByIdAsync(id);
            if (blog == null) return NotFound();
            var allBlogs = await _apiService.GetBlogsAsync();
            ViewBag.RecentBlogs = allBlogs.Where(x => x.BlogPostId != id).Take(3).ToList();
            return View(blog);
        }
    }
}