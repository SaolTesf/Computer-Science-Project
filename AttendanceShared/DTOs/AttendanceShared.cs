using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AttendanceShared.DTOs;

public class RegisterDTO {
  public string ID { get; set; } = null!;
  public string FirstName { get; set; } = null!;
  public string LastName { get; set; } = null!;
  public string Username { get; set; } = null!;
  public string Email { get; set; } = null!;
  public string Password { get; set; } = null!;
}

public class LoginDTO {
  public string Identifier { get; set; } = null!;
  public string Password { get; set; } = null!;
}

public class AuthResponseDTO {
  public string Token { get; set; } = null!;
  public ProfessorDTO User { get; set; } = null!;
}

// Simple DTO for Professor to avoid circular reference
public class ProfessorDTO {
  public string ID { get; set; } = null!;
  public string FirstName { get; set; } = null!;
  public string LastName { get; set; } = null!;
  public string Username { get; set; } = null!;
  public string Email { get; set; } = null!;
}

public class StudentDTO{
    public string UTDID { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Username { get; set; } = null!;
}

public class AttendanceDTO{
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
    public ICollection<QuizResponseDTO> QuizResponses { get; set; } = new List<QuizResponseDTO>();

    [JsonIgnore]
    public ClassSessionDTO? ClassSession { get; set; } = null!;
}

public class CourseDTO{
    [Key]
    [StringLength(10)]
    public string CourseNumber { get; set; } = string.Empty;

    [Required]
    [StringLength(255)]
    public string CourseName { get; set; } = string.Empty;

    [Required]
    [StringLength(10)]
    public string Section { get; set; } = string.Empty;

    [Required]
    [StringLength(10)]
    public string ProfessorID { get; set; } = string.Empty; // Foreign key to Professor

    // Navigation property to ClassSessions for this Course
    public ICollection<ClassSessionDTO> ClassSessions { get; set; } = new List<ClassSessionDTO>();

    // Navigation property to QuizQuestionBanks for this Course
    public ICollection<QuizQuestionBankDTO> QuizQuestionBanks { get; set; } = new List<QuizQuestionBankDTO>();
}

public class ClassSessionDTO{
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
    public QuizQuestionBankDTO? QuizQuestionBank { get; set; } = null!;

    // Navigation property to the associated Course
    [JsonIgnore]
    public CourseDTO? Course { get; set; } = null!;

    public ICollection<AttendanceDTO> Attendance { get; set; } = new List<AttendanceDTO>();
}

public class QuizQuestionBankDTO{
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
    public CourseDTO? Course { get; set; } = null!;

    // Navigation property to QuizQuestions in this bank
    public ICollection<QuizQuestionDTO> QuizQuestions { get; set; } = new List<QuizQuestionDTO>();

    // Navigation property to ClassSessions that use this bank
    public ICollection<ClassSessionDTO> ClassSessions { get; set; } = new List<ClassSessionDTO>();
}
public class QuizQuestionDTO{
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

    [JsonIgnore]
    public QuizQuestionBankDTO? QuizQuestionBank { get; set; } = null!;

    // Navigation property to QuizResponses for this question
    public ICollection<QuizResponseDTO> QuizResponses { get; set; } = new List<QuizResponseDTO>();
}

public class QuizResponseDTO{
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
    public AttendanceDTO? Attendance { get; set; } = null!;

    // Navigation property to QuizQuestion
    [JsonIgnore]
    public QuizQuestionDTO? QuizQuestion { get; set; } = null!;
}

public enum AttendanceType
{
    Present,
    Absent,
    Excused
}
