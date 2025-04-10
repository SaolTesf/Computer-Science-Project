using AttendanceSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace AttendanceSystem.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
  public DbSet<Professor> Professors { get; set; } = null!;
  public DbSet<Attendance> Attendances { get; set; } = null!;
  public DbSet<Student> Students { get; set; } = null!;
    
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
    }
    );

    modelBuilder.Entity<Student>(entity => {
      entity.ToTable("Student");
      entity.HasKey(e => e.UTDID);
      entity.Property(e => e.UTDID).HasMaxLength(10).IsRequired();
      entity.Property(e => e.FirstName).HasMaxLength(255).IsRequired();
      entity.Property(e => e.LastName).HasMaxLength(255).IsRequired();
    });
 modelBuilder.Entity<Attendance>(entity =>
            {
                entity.ToTable("Attendance");
                entity.HasKey(e => e.AttendanceID);
                entity.Property(e => e.AttendanceID).ValueGeneratedOnAdd();
                entity.Property(e => e.SessionID).IsRequired();  // SessionID is required
                entity.Property(e => e.UTDID).HasMaxLength(10).IsRequired();  // UTDID instead of UserID
                entity.Property(e => e.SubmissionTime).IsRequired();  // SubmissionTime instead of AttendanceDate
                entity.Property(e => e.IPAddress).HasMaxLength(45).IsRequired();
                entity.Property(e => e.AttendanceType)
                      .HasConversion<string>() // Store enum as string
                      .HasDefaultValue(AttendanceType.Present)
                      .IsRequired();
            });
  }
}
