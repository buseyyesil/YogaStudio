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
            var comments = await _apiService.GetBlogCommentsAsync(id);

            ViewBag.RecentBlogs = allBlogs.Where(x => x.BlogPostId != id).Take(3).ToList();
            ViewBag.Comments = comments;

            return View(blog);
        }

        [HttpPost]
        public async Task<IActionResult> AddComment(int blogPostId, string Name, string Email, string Content)
        {
            if (string.IsNullOrWhiteSpace(Name) || string.IsNullOrWhiteSpace(Content))
            {
                TempData["Error"] = "Ad ve yorum boş olamaz.";
                return RedirectToAction("Detail", new { id = blogPostId });
            }

            var success = await _apiService.AddBlogCommentAsync(blogPostId, Name, Email ?? "", Content);
            TempData[success ? "Success" : "Error"] = success
                ? "Yorumunuz eklendi! 🎉"
                : "Yorum eklenemedi.";

            return RedirectToAction("Detail", new { id = blogPostId });
        }
    }
}