using EntityLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class Context : DbContext
    {
        //seed dataları keyleri ve veri tabanı bağlantısı
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-N33MCG3\\SQLEXPRESS;Initial Catalog=KUSYSDB;Integrated Security=True;Trust Server Certificate=true");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.UserId).HasColumnName("UserID");
                entity.Property(e => e.RoleId).HasColumnName("RoleID");
                entity.Property(e => e.UserName).HasMaxLength(100);

                entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(o => o.Role)
                .WithMany(m => m.Users)
                .HasForeignKey(f => f.RoleId)
                .HasConstraintName("FK_User_Roles");
            });
            modelBuilder.Entity<User>().HasData
                (
                new User() { UserId = 1, UserName = "admin", Password = "admin", Name = "admin", LastName = "admin", RoleId = 1 },
                new User() { UserId = 2, UserName = "user", Password = "user", Name = "user", LastName = "user", RoleId = 2 },
                new User() { UserId = 3, UserName = "hakan", Password = "12345", Name = "Hakan Emre", LastName = "Yeşil", RoleId = 2, StudentId = 1 }
                );

            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.RoleId).HasColumnName("RoleID");
                entity.Property(e => e.RoleName).IsRequired().HasMaxLength(50);
            });
            modelBuilder.Entity<Role>().HasData
                (
                new Role() { RoleId = 1, RoleName = "Admin" },
                new Role() { RoleId = 2, RoleName = "User" }
                );

            modelBuilder.Entity<Student>(entity =>
            {
                entity.Property(p => p.StudentId).HasColumnName("StudentID");
                entity.Property(p => p.CourseId).HasColumnName("CourseID").HasColumnType("NCHAR").HasMaxLength(6);
                entity.Property(p => p.FirstName).IsRequired().HasMaxLength(50);
                entity.Property(p => p.LastName).IsRequired().HasMaxLength(50);
                entity.Property(p => p.BirthDate).HasColumnType("datetime");

                modelBuilder.Entity<Student>()
                .HasOne(u => u.User)
                .WithOne(st => st.Student)
           .HasForeignKey<User>(us => us.StudentId);


                entity.HasOne(o => o.Course)
                .WithMany(m => m.Students)
                .HasForeignKey(f => f.CourseId)
                .HasConstraintName("FK_Student_Courses");
            });
            modelBuilder.Entity<Student>().HasData(
                new Student()
                {
                    StudentId = 1,
                    FirstName = "Hakan Emre",
                    LastName = "Yeşil",
                    BirthDate = DateTime.Now.AddYears(-18),
                    CourseId = "MAT101"
                }
                );

            modelBuilder.Entity<Course>(entity =>
            {
                entity.Property(p => p.CourseId).HasColumnName("CourseID").HasColumnType("NCHAR").HasMaxLength(6);
                entity.Property(p => p.CourseName).IsRequired().HasMaxLength(50);

            });

            modelBuilder.Entity<Course>().HasData
                (
                new Course() { CourseId = "CSI101", CourseName = "Introduction to Computer Science" },
                new Course() { CourseId = "CSI102", CourseName = "Algorithms" },
                new Course() { CourseId = "MAT101", CourseName = "Calculus" },
                new Course() { CourseId = "PHY101", CourseName = "Physics" }
                );
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

    }
}
