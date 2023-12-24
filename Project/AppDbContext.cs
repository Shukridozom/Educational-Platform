using Microsoft.EntityFrameworkCore;
using Project.Models;
using static Org.BouncyCastle.Math.EC.ECCurve;

namespace Project
{
    public class AppDbContext : DbContext
    {
        private readonly IConfiguration _config;

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Lecture> Lectures { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options, IConfiguration config)
            : base(options)
        {
            _config = config;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // User Entity:
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();

            modelBuilder.Entity<User>()
                .Property(u => u.Username)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<User>()
                .Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(255);

            modelBuilder.Entity<User>()
                .Property(u => u.PasswordHash)
                .IsRequired()
                .HasMaxLength(255);

            modelBuilder.Entity<User>()
                .Property(u => u.FirstName)
                .IsRequired()
                .HasMaxLength(255);

            modelBuilder.Entity<User>()
                .Property(u => u.LastName)
                .IsRequired()
                .HasMaxLength(255);


            //Role Entity:
            modelBuilder.Entity<Role>()
                .HasMany(r => r.Users)
                .WithOne(u => u.Role)
                .HasForeignKey(u => u.RoleId);


            //Course Entity:
            modelBuilder.Entity<Course>()
                .Property(c => c.Title)
                .HasMaxLength(128)
                .IsRequired();

            modelBuilder.Entity<Course>()
                .Property(c => c.Description)
                .HasMaxLength(512)
                .IsRequired();

            modelBuilder.Entity<Course>()
                .HasOne(c => c.User)
                .WithMany(u => u.Courses)
                .HasForeignKey(c => c.UserId);


            //Lecture Entity:
            modelBuilder.Entity<Lecture>()
                .Property(l => l.Title)
                .HasMaxLength(128)
                .IsRequired();

            modelBuilder.Entity<Lecture>()
                .Property(l => l.Body)
                .HasMaxLength(4096)
                .IsRequired();

            modelBuilder.Entity<Lecture>()
                .HasOne(l => l.Course)
                .WithMany(c => c.Lectures)
                .HasForeignKey(l => l.CourseId);

            modelBuilder.Entity<Lecture>()
                .HasKey(l => new { l.CourseId, l.Id});



            base.OnModelCreating(modelBuilder);
        }
    }
}
