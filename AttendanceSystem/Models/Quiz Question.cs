using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

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

        // Option3 and Option4 are optional
        public string? Option3 { get; set; }
        public string? Option4 { get; set; }

        // Navigation property to the associated QuizQuestionBank
      
        public QuizQuestionBank QuizQuestionBank { get; set; } = null!;

        // Navigation property to QuizResponses for this question
        public ICollection<QuizResponse> QuizResponses { get; set; } = new List<QuizResponse>();
    }
}
