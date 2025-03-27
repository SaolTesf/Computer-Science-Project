using System;
using System.ComponentModel.DataAnnotations;

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
        [StringLength(10, MinimumLength = 1, ErrorMessage = "UserID must be between 1 and 10 characters.")]
        public string UserID { get; set; } = string.Empty;
        [Required]
        [StringLength(10, MinimumLength = 1, ErrorMessage = "CourseNumber must be between 1 and 10 characters.")]
        public string CourseNumber { get; set; } = string.Empty;
        [Required]
        public DateTime AttendanceDate { get; set; }
        [Required]
        public AttendanceType AttendanceType { get; set; }
    }
}
