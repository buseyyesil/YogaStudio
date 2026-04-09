namespace YogaStudio.Entity.Entities
{
    public class BlogPost
    {
        public int BlogPostId { get; set; }
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        public string ImageUrl { get; set; } = null!;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}