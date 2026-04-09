namespace YogaStudio.UI.Models
{
    public class BlogViewModel
    {
        public int BlogPostId { get; set; }
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        public string ImageUrl { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
    }
}