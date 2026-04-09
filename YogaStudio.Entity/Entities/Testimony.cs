namespace YogaStudio.Entity.Entities
{
    public class Testimony
    {
        public int TestimonyId { get; set; }
        public string Name { get; set; } = null!;
        public string Position { get; set; } = null!;
        public string Content { get; set; } = null!;
        public string ImageUrl { get; set; } = null!;
    }
}