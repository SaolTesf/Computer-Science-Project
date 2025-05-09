/*
Saol Tesfaghebriel
CourseEnrollment model for the Attendance System API.
*/

using System.ComponentModel.DataAnnotations;

namespace AttendanceSystem.Models;

public class CourseEnrollment {
  [Key]
  public int EnrollmentID { get; set; }

  [Required]
  public int? CourseID { get; set; } // Foreign key to Course

  [Required]
  [StringLength(10, ErrorMessage = "UTDID must be 10 characters.")]
  public string UTDID { get; set; } = string.Empty;

  public DateTime EnrollmentDate { get; set; } = DateTime.UtcNow;

  public Course? Course { get; set; }
  public Student? Student { get; set; } = null!;

}