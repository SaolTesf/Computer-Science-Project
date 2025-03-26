using AttendanceSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace AttendanceSystem.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
  public DbSet<Professor> Professors { get; set; } = null!;
    
  protected override void OnModelCreating(ModelBuilder modelBuilder) {

    base.OnModelCreating(modelBuilder);
    
    // Configure Professor entity
    modelBuilder.Entity<Professor>(entity => {
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
  }
}