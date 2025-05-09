using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
//Dinagaran Senthilkumar
// This class represents a class session in the attendance system.

namespace AttendanceSystem.Models
{
    public class ClassSession
    {
        [Key]
        public int SessionID { get; set; }

        [Required]
        public int? CourseID { get; set; } // foreign key to course ID

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
        
        [Required]
        [StringLength(36)]
        public string AccessCode { get; set; } = string.Empty;

        [JsonIgnore]
        public QuizQuestionBank? QuizQuestionBank { get; set; } = null!;

        // Navigation property to the associated Course
        [JsonIgnore]
        public Course? Course { get; set; } = null!;

        // Navigation property to the associated SessionQuestions
        [JsonIgnore]
        public ICollection<SessionQuestion> SessionQuestions { get; set; } = new List<SessionQuestion>();

        public ICollection<Attendance> Attendance { get; set; } = new List<Attendance>();
    }
}