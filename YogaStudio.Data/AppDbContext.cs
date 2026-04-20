using Microsoft.EntityFrameworkCore;
using YogaStudio.Entity.Entities;

namespace YogaStudio.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Trainer> Trainers { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Package> Packages { get; set; }
        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<Testimony> Testimonies { get; set; }
        public DbSet<Gallery> Galleries { get; set; }
        public DbSet<UserPackage> UserPackages { get; set; }
        public DbSet<ContactMessage> ContactMessages { get; set; }
        public DbSet<BlogComment> BlogComments { get; set; }
    }
}