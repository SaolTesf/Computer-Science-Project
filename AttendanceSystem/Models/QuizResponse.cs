using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AttendanceSystem.Models
{
    public class QuizResponse
    {
        [Key]
        public int ResponseID { get; set; }

        [Required]
        public int AttendanceID { get; set; }  // Foreign key to Attendance

        [Required]
        public int QuestionID { get; set; }    // Foreign key to QuizQuestion

        [Required]
        public int SelectedOption { get; set; }

        // Navigation property to Attendance
        [JsonIgnore]
        public Attendance? Attendance { get; set; } = null!;

        // Navigation property to QuizQuestion
        [JsonIgnore]
        public QuizQuestion? QuizQuestion { get; set; } = null!;
    }
}
