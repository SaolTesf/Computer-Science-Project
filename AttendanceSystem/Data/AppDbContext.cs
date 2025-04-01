using AttendanceSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace AttendanceSystem.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
  public DbSet<Professor> Professors { get; set; } = null!;
<<<<<<< HEAD
  public DbSet<Attendance> Attendances { get; set; } = null!;
=======
>>>>>>> dc81ff7998debd8cf4ac143e9ec715b1ae57409e

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {

    base.OnModelCreating(modelBuilder);

    // Configure Professor entity
    modelBuilder.Entity<Professor>(entity =>
    {
      entity.ToTable("Professor");
      entity.HasKey(e => e.ID);
      entity.Property(e => e.ID).HasMaxLength(10).IsRequired();
      entity.Property(e => e.FirstName).HasMaxLength(255).IsRequired();
      entity.Property(e => e.LastName).HasMaxLength(255).IsRequired();
      entity.Property(e => e.Username).HasMaxLength(25).IsRequired();
      entity.Property(e => e.Email).HasMaxLength(255).IsRequired();
      entity.Property(e => e.PasswordHash).HasMaxLength(255).IsRequired();

      // Set up unique constraints
      entity.HasIndex(e => e.Username).IsUnique();
      entity.HasIndex(e => e.Email).IsUnique();
    });
    modelBuilder.Entity<Attendance>(entity =>
    {
      entity.ToTable("Attendance");
      entity.HasKey(e => e.AttendanceID);
      entity.Property(e => e.AttendanceID).ValueGeneratedOnAdd();
      entity.Property(e => e.UserID).HasMaxLength(10).IsRequired();
      entity.Property(e => e.CourseNumber).HasMaxLength(10).IsRequired();
      entity.Property(e => e.AttendanceDate).IsRequired();
      entity.Property(e => e.AttendanceType).IsRequired();
    });
  }
}