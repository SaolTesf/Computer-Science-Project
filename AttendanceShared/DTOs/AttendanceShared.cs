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

public class CourseDTO {
  public string CourseNumber { get; set; } = null!;
  public string CourseName { get; set; } = null!;
  public string Section { get; set; } = null!;
  public string ProfessorID { get; set; } = null!;
  public int? CourseID { get; set; }
}

public class CourseEnrollmentDTO {
  public int EnrollmentID { get; set; }
  public string UTDID { get; set; } = null!;
  public int? CourseID { get; set; }
}

public class CourseEnrollmentDetailDTO {
  public int EnrollmentID { get; set; }
  public StudentDTO Student { get; set; } = null!;
}
public class ClassSessionDTO
{
    public int? CourseID { get; set; } // foreign key to course ID
    public DateTime SessionDate { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public string Password { get; set; } = string.Empty;
    public int QuestionBankID { get; set; } // Foreign key to QuizQuestionBank
}

