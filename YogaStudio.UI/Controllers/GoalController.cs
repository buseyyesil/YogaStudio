using Microsoft.AspNetCore.Mvc;
using YogaStudio.UI.Services;

namespace YogaStudio.UI.Controllers
{
    public class GoalController : Controller
    {
        private readonly ApiService _apiService;

        public GoalController(ApiService apiService)
        {
            _apiService = apiService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Recommend(string goal)
        {
            var lessons = await _apiService.GetLessonsAsync();

            var categoryMap = new Dictionary<string, List<string>>
            {
                { "weightloss", new List<string> { "Bikram Yoga", "Cardio", "Zayıflama", "Weight Loss" } },
                { "toning", new List<string> { "Güçlendirme", "Body Building", "Toning", "Pilates" } },
                { "pregnancy", new List<string> { "Hamilelik", "Yoga For Pregnant", "Prenatal" } },
                { "stress", new List<string> { "Meditasyon", "Meditation", "Relaxation", "Yin Yoga" } }
            };

            var goalNames = new Dictionary<string, string>
            {
                { "weightloss", "Zayıflamak" },
                { "toning", "Sıkılaşmak" },
                { "pregnancy", "Hamilelik" },
                { "stress", "Stres Azaltma" }
            };

            var keywords = categoryMap.ContainsKey(goal) ? categoryMap[goal] : new List<string>();

            var recommended = lessons
                .Where(x => keywords.Any(k =>
                    x.Name.Contains(k, StringComparison.OrdinalIgnoreCase) ||
                    (x.TrainerTitle != null && x.TrainerTitle.Contains(k, StringComparison.OrdinalIgnoreCase))
                ))
                .ToList();

            // Eğer eşleşen ders yoksa rastgele 3 ders öner
            if (!recommended.Any())
                recommended = lessons.Take(3).ToList();

            ViewBag.Goal = goalNames.ContainsKey(goal) ? goalNames[goal] : goal;
            ViewBag.GoalKey = goal;

            return View("Recommend", recommended);
        }
    }
}