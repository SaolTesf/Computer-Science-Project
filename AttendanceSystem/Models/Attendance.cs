using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AttendanceSystem.Models
{
    public enum AttendanceType
    {
        Present,
        Excused,
        Unexcused
    }

    public class Attendance
    {
        [Key]
        public int AttendanceID { get; set; }

        [Required]
        public int SessionID { get; set; } // Foreign key to ClassSession

        [Required]
        [StringLength(10, ErrorMessage = "UTDID must be 10 characters.")]
        public string UTDID { get; set; } = string.Empty; // Foreign key to Student

        [Required]
        public DateTime SubmissionTime { get; set; }

        [Required]
        [StringLength(45, ErrorMessage = "IPAddress must not exceed 45 characters.")]
        public string IPAddress { get; set; } = string.Empty;

        [Required]
        public AttendanceType AttendanceType { get; set; } = AttendanceType.Present;
        public ICollection<QuizResponse> QuizResponses { get; set; } = new List<QuizResponse>();

        [JsonIgnore]
        public ClassSession? ClassSession { get; set; } = null!;
    }
}
