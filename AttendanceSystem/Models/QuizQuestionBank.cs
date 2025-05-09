using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
// Dinagaran Senthilkumar
// QuizQuestionBank model representing a bank of quiz questions.
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
        public int? CourseID { get; set; } // foreign key to course ID

        // Navigation property to the associated Course
        [JsonIgnore]
        public Course? Course { get; set; } = null!;

        // Navigation property to QuizQuestions in this bank
        public ICollection<QuizQuestion> QuizQuestions { get; set; } = new List<QuizQuestion>();

        // Navigation property to ClassSessions that use this bank
        public ICollection<ClassSession> ClassSessions { get; set; } = new List<ClassSession>();
    }
}
