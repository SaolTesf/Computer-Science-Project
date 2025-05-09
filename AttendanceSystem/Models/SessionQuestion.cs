/*
Saol Tesfaghebriel
SessionQuestion model for the Attendance System API.
*/

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AttendanceSystem.Models
{
    public class SessionQuestion
    {
        [Key]
        public int SessionQuestionID { get; set; }

        [Required]
        public int SessionID { get; set; }

        [Required]
        public int QuestionID { get; set; }

        [JsonIgnore]
        public ClassSession? ClassSession { get; set; }

        [JsonIgnore]
        public QuizQuestion? QuizQuestion { get; set; }
    }
}