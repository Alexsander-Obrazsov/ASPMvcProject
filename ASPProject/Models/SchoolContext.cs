using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Test.Model.DataBase;

namespace Test.Model;

public partial class SchoolContext : DbContext
{
    public SchoolContext()
    {
    }

    public SchoolContext(DbContextOptions<SchoolContext> options)
        : base(options)
    {
	}

    public virtual DbSet<Class> Classes { get; set; }

    public virtual DbSet<Schedule> Schedules { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<Teacher> Teachers { get; set; }

    public virtual DbSet<StudentsLogin> StudentsLogins { get; set; }

    public virtual DbSet<TeachersLogin> TeachersLogins { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlite("Data Source=School.db");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Class>(entity =>
        {
            entity.Property(e => e.ClassName).HasColumnType("VARCHAR (10)");
            entity.Property(e => e.TeacherId).HasColumnName("Teacher_Id");

            entity.HasOne(d => d.Teacher).WithMany(p => p.Classes).HasForeignKey(d => d.TeacherId);
        });

        modelBuilder.Entity<Schedule>(entity =>
        {
            entity.ToTable("Schedule");

            entity.Property(e => e.ClassId).HasColumnName("Class_Id");
            entity.Property(e => e.Date).HasColumnType("DATE");
            entity.Property(e => e.Lesson).HasColumnType("VARCHAR (50)");
            entity.Property(e => e.TeachersId).HasColumnName("Teachers_Id");

            entity.HasOne(d => d.Class).WithMany(p => p.Schedules).HasForeignKey(d => d.ClassId);

            entity.HasOne(d => d.Teachers).WithMany(p => p.Schedules).HasForeignKey(d => d.TeachersId);
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasIndex(e => e.Id, "IX_Students_Id").IsUnique();

            entity.Property(e => e.ClassId).HasColumnName("Class_Id");
            entity.Property(e => e.FullName).HasColumnType("VARCHAR (100)");

            entity.HasOne(d => d.Class).WithMany(p => p.Students).HasForeignKey(d => d.ClassId);
        });

        modelBuilder.Entity<Teacher>(entity =>
        {
            entity.HasIndex(e => e.Id, "IX_Teachers_Id").IsUnique();

            entity.Property(e => e.FullName).HasColumnType("VARCHAR (100)");
        });

		modelBuilder.Entity<StudentsLogin>(entity =>
		{
			entity
				.HasNoKey()
				.ToTable("StudentsLogin");

			entity.HasIndex(e => e.Login, "IX_StudentsLogin_Login").IsUnique();

			entity.HasIndex(e => e.StudentsId, "IX_StudentsLogin_Students_Id").IsUnique();

			entity.Property(e => e.Login).HasColumnType("VARCHAR (50)");
			entity.Property(e => e.Password).HasColumnType("VARCHAR (50)");
			entity.Property(e => e.StudentsId).HasColumnName("Students_Id");
		});

		modelBuilder.Entity<TeachersLogin>(entity =>
		{
			entity
				.HasNoKey()
				.ToTable("TeachersLogin");

			entity.HasIndex(e => e.Login, "IX_TeachersLogin_Login").IsUnique();

			entity.HasIndex(e => e.TeachersId, "IX_TeachersLogin_Teachers_Id").IsUnique();

			entity.Property(e => e.Login).HasColumnType("VARCHAR (50)");
			entity.Property(e => e.Password).HasColumnType("VARCHAR (50)");
			entity.Property(e => e.TeachersId).HasColumnName("Teachers_Id");
		});

		OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
