using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YogaStudio.API.DTOs.LessonDtos;
using YogaStudio.Data;
using YogaStudio.Entity.Entities;

namespace YogaStudio.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LessonController : ControllerBase
    {
        private readonly AppDbContext _context;

        public LessonController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetLessons()
        {
            var values = _context.Lessons
                .Include(x => x.Trainer)
                .Select(x => new
                {
                    x.LessonId,
                    x.Name,
                    x.Date,
                    x.Capacity,
                    x.TrainerId,
                    TrainerName = x.Trainer != null ? x.Trainer.Name : null,
                    TrainerTitle = x.Trainer != null ? x.Trainer.Title : null
                })
                .ToList();

            return Ok(values);
        }

        [HttpGet("{id}")]
        public IActionResult GetLessonById(int id)
        {
            var value = _context.Lessons
                .Include(x => x.Trainer)
                .Where(x => x.LessonId == id)
                .Select(x => new
                {
                    x.LessonId,
                    x.Name,
                    x.Date,
                    x.Capacity,
                    x.TrainerId,
                    TrainerName = x.Trainer != null ? x.Trainer.Name : null,
                    TrainerTitle = x.Trainer != null ? x.Trainer.Title : null
                })
                .FirstOrDefault();

            if (value == null)
            {
                return NotFound("Ders bulunamadı");
            }

            return Ok(value);
        }

        [HttpPost]
        public IActionResult AddLesson(CreateLessonDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
            {
                return BadRequest("Ders adı boş olamaz");
            }

            if (dto.Capacity <= 0)
            {
                return BadRequest("Kapasite 0'dan büyük olmalı");
            }

            var trainerExists = _context.Trainers.Any(x => x.TrainerId == dto.TrainerId);

            if (!trainerExists)
            {
                return NotFound("Eğitmen bulunamadı");
            }

            var lesson = new Lesson
            {
                Name = dto.Name,
                TrainerId = dto.TrainerId,
                Date = dto.Date,
                Capacity = dto.Capacity,
                ZoomLink = dto.ZoomLink
            };

            _context.Lessons.Add(lesson);
            _context.SaveChanges();

            return Ok(new
            {
                message = "Ders eklendi",
                lessonId = lesson.LessonId
            });
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteLesson(int id)
        {
            var value = _context.Lessons.FirstOrDefault(x => x.LessonId == id);

            if (value == null)
            {
                return NotFound("Ders bulunamadı");
            }

            _context.Lessons.Remove(value);
            _context.SaveChanges();

            return Ok("Ders silindi");
        }

        [HttpPut("{id}")]
        public IActionResult UpdateLesson(int id, CreateLessonDto dto)
        {
            var value = _context.Lessons.FirstOrDefault(x => x.LessonId == id);

            if (value == null)
            {
                return NotFound("Ders bulunamadı");
            }

            var trainerExists = _context.Trainers.Any(x => x.TrainerId == dto.TrainerId);

            if (!trainerExists)
            {
                return NotFound("Eğitmen bulunamadı");
            }

            value.Name = dto.Name;
            value.Date = dto.Date;
            value.Capacity = dto.Capacity;
            value.TrainerId = dto.TrainerId;
            value.ZoomLink = dto.ZoomLink;
            _context.SaveChanges();

            return Ok("Ders güncellendi");
        }
    }
}