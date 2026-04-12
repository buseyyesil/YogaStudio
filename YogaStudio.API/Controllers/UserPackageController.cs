using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YogaStudio.Data;
using YogaStudio.Entity.Entities;

namespace YogaStudio.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserPackageController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UserPackageController(AppDbContext context)
        {
            _context = context;
        }

        // Kullanıcının paketlerini getir
        [HttpGet("user/{userId}")]
        public IActionResult GetUserPackages(int userId)
        {
            var values = _context.UserPackages
                .Include(x => x.Package)
                .Where(x => x.UserId == userId && x.IsActive)
                .Select(x => new
                {
                    x.UserPackageId,
                    x.UserId,
                    x.PackageId,
                    PackageName = x.Package != null ? x.Package.Name : null,
                    x.RemainingLessons,
                    x.PurchasedAt,
                    x.IsActive
                })
                .ToList();

            return Ok(values);
        }

        // Paket satın al
        [HttpPost("purchase")]
        public IActionResult PurchasePackage(PurchasePackageDto dto)
        {
            var user = _context.Users.FirstOrDefault(x => x.UserId == dto.UserId);
            if (user == null) return NotFound("Kullanıcı bulunamadı");

            var package = _context.Packages.FirstOrDefault(x => x.PackageId == dto.PackageId);
            if (package == null) return NotFound("Paket bulunamadı");

            var userPackage = new UserPackage
            {
                UserId = dto.UserId,
                PackageId = dto.PackageId,
                RemainingLessons = package.LessonCount,
                PurchasedAt = DateTime.Now,
                IsActive = true
            };

            _context.UserPackages.Add(userPackage);
            _context.SaveChanges();

            return Ok(new
            {
                message = "Paket satın alındı!",
                userPackageId = userPackage.UserPackageId,
                remainingLessons = userPackage.RemainingLessons
            });
        }

        // Ders hakkı düş
        [HttpPut("use/{userPackageId}")]
        public IActionResult UseLessonRight(int userPackageId)
        {
            var userPackage = _context.UserPackages.FirstOrDefault(x => x.UserPackageId == userPackageId);
            if (userPackage == null) return NotFound("Paket bulunamadı");

            if (userPackage.RemainingLessons <= 0)
                return BadRequest("Ders hakkınız kalmadı");

            userPackage.RemainingLessons--;

            if (userPackage.RemainingLessons == 0)
                userPackage.IsActive = false;

            _context.SaveChanges();

            return Ok(new
            {
                message = "Ders hakkı kullanıldı",
                remainingLessons = userPackage.RemainingLessons
            });
        }
    }

    public class PurchasePackageDto
    {
        public int UserId { get; set; }
        public int PackageId { get; set; }
    }
}