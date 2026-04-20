using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YogaStudio.Data;
using YogaStudio.Entity.Entities;

namespace YogaStudio.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BlogCommentController : ControllerBase
    {
        private readonly AppDbContext _context;

        public BlogCommentController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("{blogPostId}")]
        public IActionResult GetCommentsByBlog(int blogPostId)
        {
            var values = _context.BlogComments
                .Where(x => x.BlogPostId == blogPostId)
                .OrderByDescending(x => x.CreatedAt)
                .Select(x => new
                {
                    x.BlogCommentId,
                    x.BlogPostId,
                    x.Name,
                    x.Email,
                    x.Content,
                    x.CreatedAt
                })
                .ToList();

            return Ok(values);
        }

        [HttpPost]
        public IActionResult AddComment(BlogComment comment)
        {
            if (string.IsNullOrWhiteSpace(comment.Name) ||
                string.IsNullOrWhiteSpace(comment.Content))
                return BadRequest("Ad ve yorum boş olamaz.");

            comment.CreatedAt = DateTime.Now;
            _context.BlogComments.Add(comment);
            _context.SaveChanges();

            return Ok(new { message = "Yorumunuz eklendi!" });
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteComment(int id)
        {
            var value = _context.BlogComments.FirstOrDefault(x => x.BlogCommentId == id);
            if (value == null) return NotFound();
            _context.BlogComments.Remove(value);
            _context.SaveChanges();
            return Ok("Yorum silindi");
        }
    }
}