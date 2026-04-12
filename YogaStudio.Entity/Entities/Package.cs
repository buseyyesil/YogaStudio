namespace YogaStudio.Entity.Entities
{
    public class Package
    {
        public int PackageId { get; set; }
        public string Name { get; set; } = null!;
        public int LessonCount { get; set; }
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; } = true;
    }
}