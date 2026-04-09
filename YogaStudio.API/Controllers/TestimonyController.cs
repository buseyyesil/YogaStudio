using Microsoft.AspNetCore.Mvc;
using YogaStudio.Data;
using YogaStudio.Entity.Entities;

namespace YogaStudio.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestimonyController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TestimonyController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetTestimonies()
        {
            var values = _context.Testimonies.ToList();
            return Ok(values);
        }

        [HttpPost]
        public IActionResult AddTestimony(Testimony testimony)
        {
            _context.Testimonies.Add(testimony);
            _context.SaveChanges();
            return Ok(new { message = "Yorum eklendi", id = testimony.TestimonyId });
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTestimony(int id)
        {
            var value = _context.Testimonies.FirstOrDefault(x => x.TestimonyId == id);
            if (value == null) return NotFound("Yorum bulunamadı");
            _context.Testimonies.Remove(value);
            _context.SaveChanges();
            return Ok("Yorum silindi");
        }

        [HttpPut("{id}")]
        public IActionResult UpdateTestimony(int id, Testimony testimony)
        {
            var value = _context.Testimonies.FirstOrDefault(x => x.TestimonyId == id);
            if (value == null) return NotFound("Yorum bulunamadı");
            value.Name = testimony.Name;
            value.Position = testimony.Position;
            value.Content = testimony.Content;
            value.ImageUrl = testimony.ImageUrl;
            _context.SaveChanges();
            return Ok("Yorum güncellendi");
        }
    }
}