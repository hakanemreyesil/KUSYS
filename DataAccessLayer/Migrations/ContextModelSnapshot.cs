﻿// <auto-generated />
using System;
using DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DataAccessLayer.Migrations
{
    [DbContext(typeof(Context))]
    partial class ContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("EntityLayer.Course", b =>
                {
                    b.Property<string>("CourseId")
                        .HasMaxLength(6)
                        .HasColumnType("NCHAR")
                        .HasColumnName("CourseID");

                    b.Property<string>("CourseName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("CourseId");

                    b.ToTable("Courses");

                    b.HasData(
                        new
                        {
                            CourseId = "CSI101",
                            CourseName = "Introduction to Computer Science"
                        },
                        new
                        {
                            CourseId = "CSI102",
                            CourseName = "Algorithms"
                        },
                        new
                        {
                            CourseId = "MAT101",
                            CourseName = "Calculus"
                        },
                        new
                        {
                            CourseId = "PHY101",
                            CourseName = "Physics"
                        });
                });

            modelBuilder.Entity("EntityLayer.Role", b =>
                {
                    b.Property<int>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("RoleID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RoleId"));

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("RoleId");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            RoleId = 1,
                            RoleName = "Admin"
                        },
                        new
                        {
                            RoleId = 2,
                            RoleName = "User"
                        });
                });

            modelBuilder.Entity("EntityLayer.Student", b =>
                {
                    b.Property<int>("StudentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("StudentID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StudentId"));

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime");

                    b.Property<string>("CourseId")
                        .IsRequired()
                        .HasMaxLength(6)
                        .HasColumnType("NCHAR")
                        .HasColumnName("CourseID");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("StudentId");

                    b.HasIndex("CourseId");

                    b.ToTable("Students");

                    b.HasData(
                        new
                        {
                            StudentId = 1,
                            BirthDate = new DateTime(2005, 4, 5, 15, 32, 54, 499, DateTimeKind.Local).AddTicks(9047),
                            CourseId = "MAT101",
                            FirstName = "Hakan Emre",
                            LastName = "Yeşil"
                        });
                });

            modelBuilder.Entity("EntityLayer.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("UserID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int")
                        .HasColumnName("RoleID");

                    b.Property<int?>("StudentId")
                        .HasColumnType("int");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("UserId");

                    b.HasIndex("RoleId");

                    b.HasIndex("StudentId")
                        .IsUnique()
                        .HasFilter("[StudentId] IS NOT NULL");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            UserId = 1,
                            LastName = "admin",
                            Name = "admin",
                            Password = "admin",
                            RoleId = 1,
                            UserName = "admin"
                        },
                        new
                        {
                            UserId = 2,
                            LastName = "user",
                            Name = "user",
                            Password = "user",
                            RoleId = 2,
                            UserName = "user"
                        },
                        new
                        {
                            UserId = 3,
                            LastName = "Yeşil",
                            Name = "Hakan Emre",
                            Password = "12345",
                            RoleId = 2,
                            StudentId = 1,
                            UserName = "hakan"
                        });
                });

            modelBuilder.Entity("EntityLayer.Student", b =>
                {
                    b.HasOne("EntityLayer.Course", "Course")
                        .WithMany("Students")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Student_Courses");

                    b.Navigation("Course");
                });

            modelBuilder.Entity("EntityLayer.User", b =>
                {
                    b.HasOne("EntityLayer.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_User_Roles");

                    b.HasOne("EntityLayer.Student", "Student")
                        .WithOne("User")
                        .HasForeignKey("EntityLayer.User", "StudentId");

                    b.Navigation("Role");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("EntityLayer.Course", b =>
                {
                    b.Navigation("Students");
                });

            modelBuilder.Entity("EntityLayer.Role", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("EntityLayer.Student", b =>
                {
                    b.Navigation("User")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}