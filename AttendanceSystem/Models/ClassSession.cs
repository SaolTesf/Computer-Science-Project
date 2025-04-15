using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AttendanceSystem.Models
{
    public class ClassSession
    {
        [Key]
        public int SessionID { get; set; }

        [Required]
        [StringLength(10)]
        public string CourseNumber { get; set; } = string.Empty; // Foreign key to Course

        [Required]
        public DateTime SessionDateTime { get; set; }

        [Required]
        [StringLength(255)]
        public string Password { get; set; } = string.Empty;

        [Required]
        public DateTime QuizStartTime { get; set; }

        [Required]
        public DateTime QuizEndTime { get; set; }

        [Required]
        public int QuestionBankID { get; set; } // Foreign key to QuizQuestionBank

        // Navigation property to the associated Course
        public Course Course { get; set; } = null!;

        // Navigation property to the associated QuizQuestionBank
        // public QuizQuestionBank QuizQuestionBank { get; set; } = null!;

        // Optionally: Navigation property for Attendance records for this session
        public ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();
    }
}
