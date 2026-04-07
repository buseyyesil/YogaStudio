using Microsoft.AspNetCore.Mvc;
using YogaStudio.Data;

namespace YogaStudio.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PackageController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PackageController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetPackages()
        {
            var values = _context.Packages.ToList();
            return Ok(values);
        }
    }
}