using Microsoft.AspNetCore.Mvc;
using YogaStudio.Data;
using YogaStudio.Entity.Entities;

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

        [HttpGet("{id}")]
        public IActionResult GetPackageById(int id)
        {
            var value = _context.Packages.FirstOrDefault(x => x.PackageId == id);
            if (value == null) return NotFound("Paket bulunamadı");
            return Ok(value);
        }

        [HttpPost]
        public IActionResult AddPackage(Package package)
        {
            _context.Packages.Add(package);
            _context.SaveChanges();
            return Ok(new { message = "Paket eklendi", id = package.PackageId });
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePackage(int id)
        {
            var value = _context.Packages.FirstOrDefault(x => x.PackageId == id);
            if (value == null) return NotFound("Paket bulunamadı");
            _context.Packages.Remove(value);
            _context.SaveChanges();
            return Ok("Paket silindi");
        }

        [HttpPut("{id}")]
        public IActionResult UpdatePackage(int id, Package package)
        {
            var value = _context.Packages.FirstOrDefault(x => x.PackageId == id);
            if (value == null) return NotFound("Paket bulunamadı");
            value.Name = package.Name;
            value.LessonCount = package.LessonCount;
            value.Price = package.Price;
            _context.SaveChanges();
            return Ok("Paket güncellendi");
        }
    }
}