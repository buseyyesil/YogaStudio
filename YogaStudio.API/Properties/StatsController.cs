using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YogaStudio.Data;

namespace YogaStudio.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StatsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public StatsController(AppDbContext context)
        {
            _context = context;
        }

        // Aylık rezervasyon sayısı (geçmiş + gelecek 6 ay)
        [HttpGet("monthly-reservations")]
        public IActionResult GetMonthlyReservations()
        {
            var data = _context.Reservations
                .Include(x => x.Lesson)
                .Where(x => x.Lesson != null)
                .GroupBy(x => new { x.Lesson!.Date.Year, x.Lesson.Date.Month })
                .Select(g => new
                {
                    Year = g.Key.Year,
                    Month = g.Key.Month,
                    Count = g.Count()
                })
                .OrderBy(x => x.Year)
                .ThenBy(x => x.Month)
                .ToList();

            return Ok(data);
        }

        // Ders bazlı rezervasyon dağılımı
        [HttpGet("lesson-distribution")]
        public IActionResult GetLessonDistribution()
        {
            var data = _context.Reservations
                .Include(x => x.Lesson)
                .Where(x => x.Lesson != null)
                .GroupBy(x => x.Lesson!.Name)
                .Select(g => new
                {
                    LessonName = g.Key,
                    Count = g.Count()
                })
                .OrderByDescending(x => x.Count)
                .Take(6)
                .ToList();

            return Ok(data);
        }
    }
}