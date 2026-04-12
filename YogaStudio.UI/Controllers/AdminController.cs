using Microsoft.AspNetCore.Mvc;
using YogaStudio.UI.Services;

namespace YogaStudio.UI.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApiService _apiService;

        public AdminController(ApiService apiService)
        {
            _apiService = apiService;
        }

        // Admin kontrolü - sadece Admin rolü girebilir
        private IActionResult? CheckAdmin()
        {
            var role = HttpContext.Session.GetString("Role");
            if (role != "Admin")
            {
                TempData["Error"] = "Bu sayfaya erişim yetkiniz yok.";
                return RedirectToAction("Login", "Auth");
            }
            return null;
        }

        public async Task<IActionResult> Index()
        {
            var check = CheckAdmin();
            if (check != null) return check;

            var lessons = await _apiService.GetLessonsAsync();
            var trainers = await _apiService.GetTrainersAsync();
            var reservations = await _apiService.GetReservationsAsync();
            var users = await _apiService.GetUsersAsync();

            ViewBag.LessonCount = lessons.Count;
            ViewBag.TrainerCount = trainers.Count;
            ViewBag.ReservationCount = reservations.Count;
            ViewBag.UserCount = users.Count;
            ViewBag.RecentReservations = reservations.Take(5).ToList();

            return View();
        }

   
        public async Task<IActionResult> Lessons()
        {
            var check = CheckAdmin();
            if (check != null) return check;

            var lessons = await _apiService.GetLessonsAsync();
            return View(lessons);
        }

        public async Task<IActionResult> CreateLesson()
        {
            var check = CheckAdmin();
            if (check != null) return check;

            var trainers = await _apiService.GetTrainersAsync();
            ViewBag.Trainers = trainers;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateLesson(string Name, int TrainerId, DateTime Date, int Capacity)
        {
            var check = CheckAdmin();
            if (check != null) return check;

            var success = await _apiService.CreateLessonAsync(Name, TrainerId, Date, Capacity);
            TempData[success ? "Success" : "Error"] = success ? "Ders eklendi!" : "Ders eklenemedi.";
            return RedirectToAction("Lessons");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteLesson(int id)
        {
            var check = CheckAdmin();
            if (check != null) return check;

            var success = await _apiService.DeleteLessonAsync(id);
            TempData[success ? "Success" : "Error"] = success ? "Ders silindi!" : "Ders silinemedi.";
            return RedirectToAction("Lessons");
        }


        public async Task<IActionResult> Trainers()
        {
            var check = CheckAdmin();
            if (check != null) return check;

            var trainers = await _apiService.GetTrainersAsync();
            return View(trainers);
        }

        public IActionResult CreateTrainer()
        {
            var check = CheckAdmin();
            if (check != null) return check;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateTrainer(string Name, string Surname, string Title, string ImageUrl)
        {
            var check = CheckAdmin();
            if (check != null) return check;

            var success = await _apiService.CreateTrainerAsync(Name, Surname, Title, ImageUrl);
            TempData[success ? "Success" : "Error"] = success ? "Eğitmen eklendi!" : "Eğitmen eklenemedi.";
            return RedirectToAction("Trainers");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteTrainer(int id)
        {
            var check = CheckAdmin();
            if (check != null) return check;

            var success = await _apiService.DeleteTrainerAsync(id);
            TempData[success ? "Success" : "Error"] = success ? "Eğitmen silindi!" : "Eğitmen silinemedi.";
            return RedirectToAction("Trainers");
        }

        public async Task<IActionResult> Packages()
        {
            var check = CheckAdmin();
            if (check != null) return check;

            var packages = await _apiService.GetPackagesAsync();
            return View(packages);
        }

        public IActionResult CreatePackage()
        {
            var check = CheckAdmin();
            if (check != null) return check;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreatePackage(string Name, int LessonCount, decimal Price)
        {
            var check = CheckAdmin();
            if (check != null) return check;

            var success = await _apiService.CreatePackageAsync(Name, LessonCount, Price);
            TempData[success ? "Success" : "Error"] = success ? "Paket eklendi!" : "Paket eklenemedi.";
            return RedirectToAction("Packages");
        }

        [HttpPost]
        public async Task<IActionResult> DeletePackage(int id)
        {
            var check = CheckAdmin();
            if (check != null) return check;

            var success = await _apiService.DeletePackageAsync(id);
            TempData[success ? "Success" : "Error"] = success ? "Paket silindi!" : "Paket silinemedi.";
            return RedirectToAction("Packages");
        }

     
        public async Task<IActionResult> Blogs()
        {
            var check = CheckAdmin();
            if (check != null) return check;

            var blogs = await _apiService.GetBlogsAsync();
            return View(blogs);
        }

        public IActionResult CreateBlog()
        {
            var check = CheckAdmin();
            if (check != null) return check;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateBlog(string Title, string Content, string ImageUrl)
        {
            var check = CheckAdmin();
            if (check != null) return check;

            var success = await _apiService.CreateBlogAsync(Title, Content, ImageUrl);
            TempData[success ? "Success" : "Error"] = success ? "Blog eklendi!" : "Blog eklenemedi.";
            return RedirectToAction("Blogs");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteBlog(int id)
        {
            var check = CheckAdmin();
            if (check != null) return check;

            var success = await _apiService.DeleteBlogAsync(id);
            TempData[success ? "Success" : "Error"] = success ? "Blog silindi!" : "Blog silinemedi.";
            return RedirectToAction("Blogs");
        }


        public async Task<IActionResult> Testimonies()
        {
            var check = CheckAdmin();
            if (check != null) return check;

            var testimonies = await _apiService.GetTestimoniesAsync();
            return View(testimonies);
        }

        public IActionResult CreateTestimony()
        {
            var check = CheckAdmin();
            if (check != null) return check;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateTestimony(string Name, string Position, string Content, string ImageUrl)
        {
            var check = CheckAdmin();
            if (check != null) return check;

            var success = await _apiService.CreateTestimonyAsync(Name, Position, Content, ImageUrl);
            TempData[success ? "Success" : "Error"] = success ? "Yorum eklendi!" : "Yorum eklenemedi.";
            return RedirectToAction("Testimonies");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteTestimony(int id)
        {
            var check = CheckAdmin();
            if (check != null) return check;

            var success = await _apiService.DeleteTestimonyAsync(id);
            TempData[success ? "Success" : "Error"] = success ? "Yorum silindi!" : "Yorum silinemedi.";
            return RedirectToAction("Testimonies");
        }

        public async Task<IActionResult> Reservations()
        {
            var check = CheckAdmin();
            if (check != null) return check;

            var reservations = await _apiService.GetReservationsAsync();
            return View(reservations);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteReservation(int id)
        {
            var check = CheckAdmin();
            if (check != null) return check;

            var success = await _apiService.DeleteReservationAsync(id);
            TempData[success ? "Success" : "Error"] = success ? "Rezervasyon silindi!" : "Rezervasyon silinemedi.";
            return RedirectToAction("Reservations");
        }


        public async Task<IActionResult> Users()
        {
            var check = CheckAdmin();
            if (check != null) return check;

            var users = await _apiService.GetUsersAsync();
            return View(users);
        }
    }
}