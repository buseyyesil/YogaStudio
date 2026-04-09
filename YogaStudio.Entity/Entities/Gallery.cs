namespace YogaStudio.Entity.Entities
{
    public class Gallery
    {
        public int GalleryId { get; set; }
        public string ImageUrl { get; set; } = null!;
        public string? Title { get; set; }
    }
}