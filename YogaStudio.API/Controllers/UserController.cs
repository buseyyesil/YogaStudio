using Microsoft.AspNetCore.Mvc;
using YogaStudio.API.DTOs.UserDtos;
using YogaStudio.Data;
using YogaStudio.Entity.Entities;

namespace YogaStudio.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UserController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetUsers()
        {
            var values = _context.Users
                .Select(x => new
                {
                    x.UserId,
                    x.Username,
                    x.Email,
                    x.Role
                })
                .ToList();

            return Ok(values);
        }

        [HttpPost("register")]
        public IActionResult Register(CreateUserDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Username))
                return BadRequest("Kullanıcı adı boş olamaz");

            if (string.IsNullOrWhiteSpace(dto.Email))
                return BadRequest("Email boş olamaz");

            if (string.IsNullOrWhiteSpace(dto.Password))
                return BadRequest("Şifre boş olamaz");

            var emailExists = _context.Users.Any(x => x.Email == dto.Email);

            if (emailExists)
                return BadRequest("Bu email zaten kayıtlı");

            var user = new User
            {
                Username = dto.Username,
                Email = dto.Email,
                Password = dto.Password,
                Role = dto.Role
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            return Ok(new
            {
                message = "Kayıt başarılı",
                userId = user.UserId
            });
        }

        [HttpPost("login")]
        public IActionResult Login(LoginDto dto)
        {
            var user = _context.Users
                .FirstOrDefault(x => x.Email == dto.Email && x.Password == dto.Password);

            if (user == null)
                return BadRequest("Email veya şifre hatalı");

            return Ok(new
            {
                message = "Giriş başarılı",
                userId = user.UserId,
                username = user.Username,
                role = user.Role
            });
        }
        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, [FromBody] UpdateUserDto dto)
        {
            var user = _context.Users.FirstOrDefault(x => x.UserId == id);
            if (user == null) return NotFound();
            user.Username = dto.Username;
            user.Email = dto.Email;
            _context.SaveChanges();
            return Ok("Profil güncellendi");
        }


    }
}