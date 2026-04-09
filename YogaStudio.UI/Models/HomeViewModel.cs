namespace YogaStudio.UI.Models
{
    public class HomeViewModel
    {
        public List<LessonViewModel> Lessons { get; set; } = new();
        public List<PackageViewModel> Packages { get; set; } = new();
        public List<BlogViewModel> Blogs { get; set; } = new();
        public List<TestimonyViewModel> Testimonies { get; set; } = new();
    }
}