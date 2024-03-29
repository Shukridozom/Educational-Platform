﻿using Microsoft.EntityFrameworkCore;
using Project.Core.Domains;
using static Org.BouncyCastle.Math.EC.ECCurve;

namespace Project.Persistence
{
    public class AppDbContext : DbContext
    {
        private readonly IConfiguration _config;

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<SystemVariables> SystemVariables { get; set; }
        public DbSet<TransferType> TransferTypes { get; set; }
        public DbSet<Transfer> Transfer { get; set; }

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


            //Lesson Entity:
            modelBuilder.Entity<Lesson>()
                .Property(l => l.Title)
                .HasMaxLength(128)
                .IsRequired();

            modelBuilder.Entity<Lesson>()
                .Property(l => l.Body)
                .HasMaxLength(4096)
                .IsRequired();

            modelBuilder.Entity<Lesson>()
                .HasKey(l => new { l.Id, l.CourseId });

            modelBuilder.Entity<Lesson>()
                .Property(l => l.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Lesson>()
                .HasOne(l => l.Course)
                .WithMany(c => c.Lessons)
                .HasForeignKey(l => l.CourseId);

            //Enrollment Entity:
            modelBuilder.Entity<Enrollment>()
                .HasOne(e => e.User)
                .WithMany(u => u.Enrollments)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Enrollment>()
                .HasOne(e => e.Course)
                .WithMany(c => c.Enrollments)
                .HasForeignKey(e => e.CourseId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Enrollment>()
                .HasKey(e => new { e.UserId, e.CourseId });

            //PaymentWithdraw Entity:
            modelBuilder.Entity<Transfer>()
                .HasOne(pw => pw.User)
                .WithMany(u => u.PaymentWithdraws)
                .HasForeignKey(pw => pw.UserId);

            modelBuilder.Entity<Transfer>()
                .HasOne(pw => pw.Type)
                .WithMany(t => t.PaymentWithdraws)
                .HasForeignKey(pw => pw.TypeId);


            //PaymentWithdrawType Entity:
            modelBuilder.Entity<TransferType>()
                .HasKey(pwt => pwt.Id);

            modelBuilder.Entity<TransferType>()
                .Property(pwt => pwt.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<TransferType>()
                .Property(pwt => pwt.Name)
                .HasMaxLength(128);



            base.OnModelCreating(modelBuilder);
        }
    }
}
