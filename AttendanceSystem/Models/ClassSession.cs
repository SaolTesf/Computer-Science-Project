using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

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
        
        [JsonIgnore]
        public QuizQuestionBank? QuizQuestionBank { get; set; } = null!;

        // Navigation property to the associated Course
        [JsonIgnore]
        public Course? Course { get; set; } = null!;

        public ICollection<Attendance> Attendance { get; set; } = new List<Attendance>();
    }
}