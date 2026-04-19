using Microsoft.AspNetCore.Mvc;
using YogaStudio.Data;
using YogaStudio.Entity.Entities;

namespace YogaStudio.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BlogController : ControllerBase
    {
        private readonly AppDbContext _context;

        public BlogController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetBlogs()
        {
            var values = _context.BlogPosts.ToList();
            return Ok(values);
        }

        [HttpGet("{id}")]
        public IActionResult GetBlogById(int id)
        {
            var value = _context.BlogPosts.FirstOrDefault(x => x.BlogPostId == id);
            if (value == null) return NotFound("Blog yazısı bulunamadı");
            return Ok(value);
        }

        [HttpPost]
        public IActionResult AddBlog(BlogPost blog)
        {
            _context.BlogPosts.Add(blog);
            _context.SaveChanges();
            return Ok(new { message = "Blog yazısı eklendi", id = blog.BlogPostId });
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBlog(int id)
        {
            var value = _context.BlogPosts.FirstOrDefault(x => x.BlogPostId == id);
            if (value == null) return NotFound("Blog yazısı bulunamadı");
            _context.BlogPosts.Remove(value);
            _context.SaveChanges();
            return Ok("Blog yazısı silindi");
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBlog(int id, BlogPost blog)
        {
            var value = _context.BlogPosts.FirstOrDefault(x => x.BlogPostId == id);
            if (value == null) return NotFound("Blog yazısı bulunamadı");
            value.Title = blog.Title;
            value.Content = blog.Content;
            value.ImageUrl = blog.ImageUrl;
            value.CreatedAt = blog.CreatedAt;
            _context.SaveChanges();
            return Ok("Blog yazısı güncellendi");
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var value = _context.BlogPosts.FirstOrDefault(x => x.BlogPostId == id);
            if (value == null) return NotFound();
            return Ok(value);
        }
    }
}