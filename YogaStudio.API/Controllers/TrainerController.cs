using Microsoft.AspNetCore.Mvc;
using YogaStudio.Data;
using YogaStudio.Entity.Entities;

namespace YogaStudio.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TrainerController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TrainerController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetTrainers()
        {
            var values = _context.Trainers.ToList();
            return Ok(values);
        }

        [HttpGet("{id}")]
        public IActionResult GetTrainerById(int id)
        {
            var value = _context.Trainers.FirstOrDefault(x => x.TrainerId == id);
            if (value == null) return NotFound("Eğitmen bulunamadı");
            return Ok(value);
        }

        [HttpPost]
        public IActionResult AddTrainer(Trainer trainer)
        {
            _context.Trainers.Add(trainer);
            _context.SaveChanges();
            return Ok(new { message = "Eğitmen eklendi", id = trainer.TrainerId });
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTrainer(int id)
        {
            var value = _context.Trainers.FirstOrDefault(x => x.TrainerId == id);
            if (value == null) return NotFound("Eğitmen bulunamadı");
            _context.Trainers.Remove(value);
            _context.SaveChanges();
            return Ok("Eğitmen silindi");
        }

        [HttpPut("{id}")]
        public IActionResult UpdateTrainer(int id, Trainer trainer)
        {
            var value = _context.Trainers.FirstOrDefault(x => x.TrainerId == id);
            if (value == null) return NotFound("Eğitmen bulunamadı");
            value.Name = trainer.Name;
            value.Surname = trainer.Surname;
            value.Title = trainer.Title;
            value.ImageUrl = trainer.ImageUrl;
            _context.SaveChanges();
            return Ok("Eğitmen güncellendi");
        }
    }
}