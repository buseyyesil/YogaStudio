namespace YogaStudio.Entity.Entities
{
    public class BlogComment
    {
        public int BlogCommentId { get; set; }
        public int BlogPostId { get; set; }
        public BlogPost? BlogPost { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Content { get; set; } = null!;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}