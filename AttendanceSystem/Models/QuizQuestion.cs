using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
//Dinagaran Senthilkumar
// QuizQuestion model representing a question in a quiz.
namespace AttendanceSystem.Models
{
    public class QuizQuestion
    {
        [Key]
        public int QuestionID { get; set; }

        [Required]
        public int QuestionBankID { get; set; } // Foreign key to QuizQuestionBank

        [Required]
        public string QuestionText { get; set; } = string.Empty;

        [Required]
        public string Option1 { get; set; } = string.Empty;

        [Required]
        public string Option2 { get; set; } = string.Empty;

        [Required]
        public int Answer { get; set; } = 0;

        // Option3 and Option4 are optional
        public string? Option3 { get; set; }
        public string? Option4 { get; set; }

        // Navigation property to the associated QuizQuestionBank

        [JsonIgnore]
        public QuizQuestionBank? QuizQuestionBank { get; set; } = null!;

        // Navigation property to the associated SessionQuestions
        [JsonIgnore]
        public ICollection<SessionQuestion> SessionQuestions { get; set; } = new List<SessionQuestion>();

        // Navigation property to QuizResponses for this question
        public ICollection<QuizResponse> QuizResponses { get; set; } = new List<QuizResponse>();
    }
}
