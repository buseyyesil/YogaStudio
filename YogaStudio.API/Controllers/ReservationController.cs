using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YogaStudio.Data;
using YogaStudio.Entity.Entities;

namespace YogaStudio.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;

        public ReservationController(AppDbContext context)
        {
            _appDbContext = context;
        }

        [HttpGet]
        public IActionResult GetReservations()
        {
            var values = _appDbContext.Reservations
                .Include(x => x.User)
                .Include(x => x.Lesson)
                .ToList();

            return Ok(values);
        }

        [HttpGet("{id}")]
        public IActionResult GetReservationById(int id)
        {
            var value = _appDbContext.Reservations
                .Include(x => x.User)
                .Include(x => x.Lesson)
                .FirstOrDefault(x => x.ReservationId == id);

            if (value == null)
            {
                return NotFound("Rezervasyon bulunamadı");
            }

            return Ok(value);
        }

        [HttpPost]
        public IActionResult AddReservation(Reservation reservation)
        {
            var lesson = _appDbContext.Lessons
                .Include(x => x.Reservations)
                .FirstOrDefault(x => x.LessonId == reservation.LessonId);

            if (lesson == null)
            {
                return NotFound("Ders bulunamadı");
            }

            var user = _appDbContext.Users.FirstOrDefault(x => x.UserId == reservation.UserId);

            if (user == null)
            {
                return NotFound("Kullanıcı bulunamadı");
            }

            if (lesson.Reservations.Count >= lesson.Capacity)
            {
                return BadRequest("Kontenjan dolu");
            }

            _appDbContext.Reservations.Add(reservation);
            _appDbContext.SaveChanges();

            return Ok("Rezervasyon başarılı");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteReservation(int id)
        {
            var value = _appDbContext.Reservations.FirstOrDefault(x => x.ReservationId == id);

            if (value == null)
            {
                return NotFound("Rezervasyon bulunamadı");
            }

            _appDbContext.Reservations.Remove(value);
            _appDbContext.SaveChanges();

            return Ok("Rezervasyon silindi");
        }
    }
}
