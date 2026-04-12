using Microsoft.AspNetCore.Mvc;
using YogaStudio.Data;
using YogaStudio.Entity.Entities;

namespace YogaStudio.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ContactController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetMessages()
        {
            var values = _context.ContactMessages
                .OrderByDescending(x => x.CreatedAt)
                .ToList();
            return Ok(values);
        }

        [HttpPost]
        public IActionResult SendMessage(ContactMessage message)
        {
            if (string.IsNullOrWhiteSpace(message.Name) ||
                string.IsNullOrWhiteSpace(message.Email) ||
                string.IsNullOrWhiteSpace(message.Message))
            {
                return BadRequest("Ad, email ve mesaj boş olamaz.");
            }

            message.CreatedAt = DateTime.Now;
            message.IsRead = false;

            _context.ContactMessages.Add(message);
            _context.SaveChanges();

            return Ok(new { message = "Mesajınız gönderildi!" });
        }

        [HttpPut("read/{id}")]
        public IActionResult MarkAsRead(int id)
        {
            var value = _context.ContactMessages.FirstOrDefault(x => x.ContactMessageId == id);
            if (value == null) return NotFound("Mesaj bulunamadı");
            value.IsRead = true;
            _context.SaveChanges();
            return Ok("Mesaj okundu olarak işaretlendi");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteMessage(int id)
        {
            var value = _context.ContactMessages.FirstOrDefault(x => x.ContactMessageId == id);
            if (value == null) return NotFound("Mesaj bulunamadı");
            _context.ContactMessages.Remove(value);
            _context.SaveChanges();
            return Ok("Mesaj silindi");
        }
    }
}