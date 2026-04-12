namespace YogaStudio.Entity.Entities
{
    public class UserPackage
    {
        public int UserPackageId { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }
        public int PackageId { get; set; }
        public Package? Package { get; set; }
        public int RemainingLessons { get; set; }
        public DateTime PurchasedAt { get; set; } = DateTime.Now;
        public bool IsActive { get; set; } = true;
    }
}