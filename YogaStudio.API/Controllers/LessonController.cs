using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YogaStudio.Data;
using YogaStudio.Entity.Entities;

namespace YogaStudio.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LessonController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        public LessonController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        [HttpGet]
        public IActionResult GetLessons()
        {
            var values=_appDbContext.Lessons.ToList();
            return Ok(values);
        }
        [HttpGet("{id}")]
        public IActionResult GetLessons(int id)
        {
            var value = _appDbContext.Lessons.FirstOrDefault(x => x.LessonId == id);
            if (value == null)
            {
                return NotFound("Ders Bulunamadı");
            }
            return Ok("value");

        }
        [HttpPost]
        public IActionResult AddLesson(Lesson lesson)
        {
            _appDbContext.Lessons.Add(lesson);
            _appDbContext.SaveChanges();
            return Ok("Ders Eklendi");    
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteLesson(int id)
        {
            var value = _appDbContext.Lessons.FirstOrDefault(x => x.LessonId == id);

            if (value == null)
            {
                return NotFound("Ders bulunamadı");
            }

            _appDbContext.Lessons.Remove(value);
            _appDbContext.SaveChanges();
            return Ok("Ders silindi");
        }

        [HttpPut]
        public IActionResult UpdateLesson(Lesson lesson)
        {
            var value = _appDbContext.Lessons.FirstOrDefault(x => x.LessonId == lesson.LessonId);

            if (value == null)
            {
                return NotFound("Ders bulunamadı");
            }

            value.Name = lesson.Name;
            value.Date = lesson.Date;
            value.Capacity = lesson.Capacity;
            value.TrainerId = lesson.TrainerId;

            _appDbContext.SaveChanges();
            return Ok("Ders güncellendi");
        }
    }

}
