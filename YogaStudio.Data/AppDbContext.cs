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
    }
}