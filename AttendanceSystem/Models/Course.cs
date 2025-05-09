using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
//Dinagaran Senthilkumar
// Course model representing a course in the attendance system.
namespace AttendanceSystem.Models
{
    public class Course
    {
        [Key]
        public int? CourseID { get; set; } // auto incremented PK

        [Required]
        [StringLength(10)]
        public string CourseNumber { get; set; } = string.Empty;

        [Required]
        [StringLength(255)]
        public string CourseName { get; set; } = string.Empty;

        [Required]
        [StringLength(10)]
        public string Section { get; set; } = string.Empty;

        [Required]
        [StringLength(10)]
        public string ProfessorID { get; set; } = string.Empty; // Foreign key to Professor

        // Navigation property to ClassSessions for this Course
        public ICollection<ClassSession> ClassSessions { get; set; } = new List<ClassSession>();

        // Navigation property to QuizQuestionBanks for this Course
        public ICollection<QuizQuestionBank> QuizQuestionBanks { get; set; } = new List<QuizQuestionBank>();

        public ICollection<CourseEnrollment>? CourseEnrollments { get; set; } = new List<CourseEnrollment>();    
    }
}
