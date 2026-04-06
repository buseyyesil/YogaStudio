using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YogaStudio.API.DTOs.ReservationDtos;
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
                .Select(x => new
                {
                    x.ReservationId,
                    x.UserId,
                    UserName = x.User != null ? x.User.Username : null,
                    x.LessonId,
                    LessonName = x.Lesson != null ? x.Lesson.Name : null
                })
                .ToList();

            return Ok(values);
        }

        [HttpGet("{id}")]
        public IActionResult GetReservationById(int id)
        {
            var value = _appDbContext.Reservations
                .Include(x => x.User)
                .Include(x => x.Lesson)
                .Where(x => x.ReservationId == id)
                .Select(x => new
                {
                    x.ReservationId,
                    x.UserId,
                    UserName = x.User != null ? x.User.Username : null,
                    x.LessonId,
                    LessonName = x.Lesson != null ? x.Lesson.Name : null
                })
                .FirstOrDefault();

            if (value == null)
            {
                return NotFound("Rezervasyon bulunamadı");
            }

            return Ok(value);
        }

        [HttpPost]
        public IActionResult AddReservation(CreateReservationDto dto)
        {
            if (dto.UserId <= 0 || dto.LessonId <= 0)
            {
                return BadRequest("Geçersiz kullanıcı veya ders bilgisi");
            }

            var lesson = _appDbContext.Lessons
                .Include(x => x.Reservations)
                .FirstOrDefault(x => x.LessonId == dto.LessonId);

            if (lesson == null)
            {
                return NotFound("Ders bulunamadı");
            }

            var user = _appDbContext.Users.FirstOrDefault(x => x.UserId == dto.UserId);

            if (user == null)
            {
                return NotFound("Kullanıcı bulunamadı");
            }

            var existingReservation = _appDbContext.Reservations
                .FirstOrDefault(x => x.UserId == dto.UserId && x.LessonId == dto.LessonId);

            if (existingReservation != null)
            {
                return BadRequest("Bu kullanıcı bu derse zaten kayıtlı");
            }

            if (lesson.Reservations.Count >= lesson.Capacity)
            {
                return BadRequest("Kontenjan dolu");
            }

            var reservation = new Reservation
            {
                UserId = dto.UserId,
                LessonId = dto.LessonId
            };

            _appDbContext.Reservations.Add(reservation);
            _appDbContext.SaveChanges();

            return Ok(new
            {
                message = "Rezervasyon başarılı",
                reservationId = reservation.ReservationId
            });
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