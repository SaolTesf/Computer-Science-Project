/*
AppDbContext class that represents the database context for the attendance system, including the Professor entity configuration.
*/

using AttendanceSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace AttendanceSystem.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
  public DbSet<Professor> Professors { get; set; } = null!;
  public DbSet<Attendance> Attendances { get; set; } = null!;
  public DbSet<Course> Courses { get; set; } = null!;
  public DbSet<ClassSession> ClassSessions { get; set; } = null!;
  public DbSet<QuizQuestionBank> QuizQuestionBanks { get; set; } = null!;
  public DbSet<QuizQuestion> QuizQuestions { get; set; } = null!;
  public DbSet<QuizResponse> QuizResponses { get; set; } = null!;
  public DbSet<Student> Students { get; set; } = null!;
  public DbSet<CourseEnrollment> CourseEnrollments { get; set; } = null!;
  public DbSet<SelectedQuizQuestion> SelectedQuizQuestions { get; set; }

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

    modelBuilder.Entity<Student>(entity =>
    {
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
                 entity.HasOne(a => a.ClassSession)
                      .WithMany(cs => cs.Attendance)
                      .HasForeignKey(a => a.SessionID)
                      .OnDelete(DeleteBehavior.Cascade);
               });
    // Configure Course entity
    modelBuilder.Entity<Course>(entity =>
    {
      entity.ToTable("Course");
      entity.HasKey(e => e.CourseID);
      entity.Property(e => e.CourseID).IsRequired();
      entity.Property(e => e.CourseNumber).HasMaxLength(10).IsRequired();
      entity.Property(e => e.CourseName).HasMaxLength(255).IsRequired();
      entity.Property(e => e.Section).HasMaxLength(10).IsRequired();
      entity.Property(e => e.ProfessorID).HasMaxLength(10).IsRequired();
    });

    // Configure ClassSession entity
    modelBuilder.Entity<ClassSession>(entity =>
    {
      entity.ToTable("ClassSession");
      entity.HasKey(e => e.SessionID);
      entity.Property(e => e.SessionID).ValueGeneratedOnAdd();
      entity.Property(e => e.CourseID).IsRequired();
      entity.Property(e => e.SessionDateTime).IsRequired();
      entity.Property(e => e.Password).HasMaxLength(255).IsRequired();
      entity.Property(e => e.QuizStartTime).IsRequired();
      entity.Property(e => e.QuizEndTime).IsRequired();
      entity.Property(e => e.QuestionBankID).IsRequired();
      entity.HasOne(e => e.Course)
                    .WithMany(c => c.ClassSessions)
                    .HasForeignKey(e => e.CourseID)
                    .OnDelete(DeleteBehavior.Cascade);

      // map QuizQuestionBank to ClassSession               
      entity.HasOne(e => e.QuizQuestionBank)
        .WithMany(qb => qb.ClassSessions)
        .HasForeignKey(e => e.QuestionBankID)
        .OnDelete(DeleteBehavior.Cascade);

      entity.HasOne(e => e.Course)
            .WithMany(c => c.ClassSessions)
            .HasForeignKey(e => e.CourseID)
            .OnDelete(DeleteBehavior.Cascade);
       
    });

    // Configure QuizQuestionBank entity
    modelBuilder.Entity<QuizQuestionBank>(entity =>
    {
      entity.ToTable("QuizQuestionBank");
      entity.HasKey(e => e.QuestionBankID);
      entity.Property(e => e.QuestionBankID).ValueGeneratedOnAdd();
      entity.Property(e => e.BankName).HasMaxLength(255).IsRequired();
      entity.Property(e => e.CourseID).IsRequired();
      entity.HasOne(e => e.Course)
                    .WithMany(c => c.QuizQuestionBanks)
                    .HasForeignKey(e => e.CourseID)
                    .OnDelete(DeleteBehavior.Cascade);
    });

    // Configure QuizQuestion entity
    modelBuilder.Entity<QuizQuestion>(entity =>
    {
      entity.ToTable("QuizQuestion");
      entity.HasKey(e => e.QuestionID);
      entity.Property(e => e.QuestionID).ValueGeneratedOnAdd();
      entity.Property(e => e.QuestionText).IsRequired();
      entity.Property(e => e.Option1).IsRequired();
      entity.Property(e => e.Option2).IsRequired();
      entity.Property(e => e.Answer).IsRequired();
        // Option3 and Option4 are optional
        entity.HasOne(e => e.QuizQuestionBank)
                    .WithMany(qb => qb.QuizQuestions)
                    .HasForeignKey(e => e.QuestionBankID)
                    .OnDelete(DeleteBehavior.Cascade);
      
    });

    // Configure QuizResponse entity
    modelBuilder.Entity<QuizResponse>(entity =>
    {
      entity.ToTable("QuizResponse");
      entity.HasKey(e => e.ResponseID);
      entity.Property(e => e.ResponseID).ValueGeneratedOnAdd();
      entity.Property(e => e.AttendanceID).IsRequired();
      entity.Property(e => e.QuestionID).IsRequired();
      entity.Property(e => e.SelectedOption).IsRequired();
      entity.HasOne(e => e.Attendance)
                    .WithMany(a => a.QuizResponses) // Include Attendance navigation if desired
                    .HasForeignKey(e => e.AttendanceID)
                    .OnDelete(DeleteBehavior.Cascade);
      entity.HasOne(e => e.QuizQuestion)
                    .WithMany(q => q.QuizResponses)
                    .HasForeignKey(e => e.QuestionID)
                    .OnDelete(DeleteBehavior.Cascade);
    });

    // Configure CourseEnrollment entity
    modelBuilder.Entity<CourseEnrollment>(entity =>
    {
      entity.ToTable("CourseEnrollment");
      entity.HasKey(e => e.EnrollmentID);
      entity.Property(e => e.CourseID).IsRequired();
      entity.Property(e => e.UTDID).HasMaxLength(10).IsRequired();
      entity.HasOne(e => e.Course)
            .WithMany(c => c.CourseEnrollments)
            .HasForeignKey(e => e.CourseID)
            .OnDelete(DeleteBehavior.Cascade);
      entity.HasOne(e => e.Student)
            .WithMany(s => s.CourseEnrollments)
            .HasForeignKey(e => e.UTDID)
            .OnDelete(DeleteBehavior.Cascade);
    });

    }
}
