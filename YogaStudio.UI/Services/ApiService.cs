using System.Text;
using System.Text.Json;
using YogaStudio.UI.Models;

namespace YogaStudio.UI.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }



        public async Task<List<LessonViewModel>> GetLessonsAsync()
        {
            var response = await _httpClient.GetAsync("api/Lesson");
            if (!response.IsSuccessStatusCode) return new List<LessonViewModel>();
            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<LessonViewModel>>(json, JsonOptions()) ?? new List<LessonViewModel>();
        }

        public async Task<LessonViewModel?> GetLessonByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"api/Lesson/{id}");
            if (!response.IsSuccessStatusCode) return null;
            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<LessonViewModel>(json, JsonOptions());
        }

        public async Task<bool> CreateLessonAsync(string name, int trainerId, DateTime date, int capacity)
        {
            var json = JsonSerializer.Serialize(new { Name = name, TrainerId = trainerId, Date = date, Capacity = capacity });
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/Lesson", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteLessonAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/Lesson/{id}");
            return response.IsSuccessStatusCode;
        }



        public async Task<List<TrainerViewModel>> GetTrainersAsync()
        {
            var response = await _httpClient.GetAsync("api/Trainer");
            if (!response.IsSuccessStatusCode) return new List<TrainerViewModel>();
            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<TrainerViewModel>>(json, JsonOptions()) ?? new List<TrainerViewModel>();
        }

        public async Task<bool> CreateTrainerAsync(string name, string surname, string title, string imageUrl)
        {
            var json = JsonSerializer.Serialize(new { Name = name, Surname = surname, Title = title, ImageUrl = imageUrl });
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/Trainer", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteTrainerAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/Trainer/{id}");
            return response.IsSuccessStatusCode;
        }

        public async Task<List<PackageViewModel>> GetPackagesAsync()
        {
            var response = await _httpClient.GetAsync("api/Package");
            if (!response.IsSuccessStatusCode) return new List<PackageViewModel>();
            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<PackageViewModel>>(json, JsonOptions()) ?? new List<PackageViewModel>();
        }

        public async Task<bool> CreatePackageAsync(string name, int lessonCount, decimal price)
        {
            var json = JsonSerializer.Serialize(new { Name = name, LessonCount = lessonCount, Price = price });
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/Package", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeletePackageAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/Package/{id}");
            return response.IsSuccessStatusCode;
        }


        public async Task<bool> CreateReservationAsync(CreateReservationViewModel model)
        {
            var json = JsonSerializer.Serialize(model);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/Reservation", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<List<ReservationViewModel>> GetReservationsAsync()
        {
            var response = await _httpClient.GetAsync("api/Reservation");
            if (!response.IsSuccessStatusCode) return new List<ReservationViewModel>();
            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<ReservationViewModel>>(json, JsonOptions()) ?? new List<ReservationViewModel>();
        }

        public async Task<bool> DeleteReservationAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/Reservation/{id}");
            return response.IsSuccessStatusCode;
        }

  

        public async Task<List<BlogViewModel>> GetBlogsAsync()
        {
            var response = await _httpClient.GetAsync("api/Blog");
            if (!response.IsSuccessStatusCode) return new List<BlogViewModel>();
            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<BlogViewModel>>(json, JsonOptions()) ?? new List<BlogViewModel>();
        }

        public async Task<bool> CreateBlogAsync(string title, string content, string imageUrl)
        {
            var json = JsonSerializer.Serialize(new { Title = title, Content = content, ImageUrl = imageUrl, CreatedAt = DateTime.Now });
            var jsonContent = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/Blog", jsonContent);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteBlogAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/Blog/{id}");
            return response.IsSuccessStatusCode;
        }



        public async Task<List<TestimonyViewModel>> GetTestimoniesAsync()
        {
            var response = await _httpClient.GetAsync("api/Testimony");
            if (!response.IsSuccessStatusCode) return new List<TestimonyViewModel>();
            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<TestimonyViewModel>>(json, JsonOptions()) ?? new List<TestimonyViewModel>();
        }

        public async Task<bool> CreateTestimonyAsync(string name, string position, string content, string imageUrl)
        {
            var json = JsonSerializer.Serialize(new { Name = name, Position = position, Content = content, ImageUrl = imageUrl });
            var jsonContent = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/Testimony", jsonContent);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteTestimonyAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/Testimony/{id}");
            return response.IsSuccessStatusCode;
        }



        public async Task<List<UserViewModel>> GetUsersAsync()
        {
            var response = await _httpClient.GetAsync("api/User");
            if (!response.IsSuccessStatusCode) return new List<UserViewModel>();
            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<UserViewModel>>(json, JsonOptions()) ?? new List<UserViewModel>();
        }

        public async Task<(bool Success, string Message, int UserId, string Username, string Role)> LoginAsync(LoginViewModel model)
        {
            var json = JsonSerializer.Serialize(new { model.Email, model.Password });
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/User/login", content);
            var responseJson = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                return (false, "Email veya şifre hatalı", 0, "", "");

            var result = JsonSerializer.Deserialize<JsonElement>(responseJson);
            return (
                true,
                "Giriş başarılı",
                result.GetProperty("userId").GetInt32(),
                result.GetProperty("username").GetString() ?? "",
                result.GetProperty("role").GetString() ?? ""
            );
        }

        public async Task<(bool Success, string Message)> RegisterAsync(RegisterViewModel model)
        {
            var json = JsonSerializer.Serialize(new
            {
                model.Username,
                model.Email,
                model.Password,
                model.Role
            });
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/User/register", content);
            var responseJson = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                return (false, responseJson);

            return (true, "Kayıt başarılı");
        }


        private static JsonSerializerOptions JsonOptions() => new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
    }
}
