using Microsoft.AspNetCore.Mvc;
using YogaStudio.Data;

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
    }
}