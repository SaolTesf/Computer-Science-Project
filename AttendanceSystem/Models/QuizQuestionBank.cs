using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AttendanceSystem.Models
{
    public class QuizQuestionBank
    {
        [Key]
        public int QuestionBankID { get; set; }

        [Required]
        [StringLength(255)]
        public string BankName { get; set; } = string.Empty;

        [Required]
        [StringLength(10)]
        public string CourseNumber { get; set; } = string.Empty; // Foreign key to Course

        // Navigation property to the associated Course
        [JsonIgnore]
        public Course? Course { get; set; } = null!;

        // Navigation property to QuizQuestions in this bank
        public ICollection<QuizQuestion> QuizQuestions { get; set; } = new List<QuizQuestion>();

        // Navigation property to ClassSessions that use this bank
        public ICollection<ClassSession> ClassSessions { get; set; } = new List<ClassSession>();
    }
}
