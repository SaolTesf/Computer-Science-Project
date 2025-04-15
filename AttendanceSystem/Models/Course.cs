using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AttendanceSystem.Models
{
    public class Course
    {
        [Key]
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

        // Navigation property to the Professor entity
        public Professor Professor { get; set; } = null!;

        // Navigation property to ClassSessions for this Course
        // ICollection<ClassSession> ClassSessions { get; set; } = new List<ClassSession>();

        // Navigation property to QuizQuestionBanks for this Course
        //public ICollection<QuizQuestionBank> QuizQuestionBanks { get; set; } = new List<QuizQuestionBank>();
    }
}
