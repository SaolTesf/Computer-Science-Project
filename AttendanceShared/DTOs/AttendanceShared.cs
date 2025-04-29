
namespace AttendanceShared.DTOs;

public class RegisterDTO
{
    public string ID { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Username { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}

public class LoginDTO
{
    public string Identifier { get; set; } = null!;
    public string Password { get; set; } = null!;
}

public class AuthResponseDTO
{
    public string Token { get; set; } = null!;
    public ProfessorDTO User { get; set; } = null!;
}

// Simple DTO for Professor to avoid circular reference
public class ProfessorDTO
{
    public string ID { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Username { get; set; } = null!;
    public string Email { get; set; } = null!;
}

public class StudentDTO
{
    public string UTDID { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Username { get; set; } = null!;
}

public class CourseDTO
{
    public string CourseNumber { get; set; } = null!;
    public string CourseName { get; set; } = null!;
    public string Section { get; set; } = null!;
    public string ProfessorID { get; set; } = null!;
    public int? CourseID { get; set; }
}

public class CourseEnrollmentDTO
{
    public int EnrollmentID { get; set; }
    public string CourseNumber { get; set; } = null!;
    public string UTDID { get; set; } = null!;
    public int? CourseID { get; set; }
}

public class CourseEnrollmentDetailDTO
{
    public int EnrollmentID { get; set; }
    public StudentDTO Student { get; set; } = null!;
}

public class AttendanceDTO{
    public int AttendanceID { get; set; }

    public int SessionID { get; set; } 

    public string UTDID { get; set; } = string.Empty; 

    public DateTime SubmissionTime { get; set; }

    public string IPAddress { get; set; } = string.Empty;

    public AttendanceType AttendanceType { get; set; } = AttendanceType.Present;
}


public class ClassSessionDTO{
    public int SessionID { get; set; }

    public string CourseNumber { get; set; } = string.Empty; 

    public DateTime SessionDateTime { get; set; }

    public string Password { get; set; } = string.Empty;

    public DateTime QuizStartTime { get; set; }

    public DateTime QuizEndTime { get; set; }

    public int QuestionBankID { get; set; }


}

public class QuizQuestionBankDTO{
    public int QuestionBankID { get; set; }
    public string BankName { get; set; } = string.Empty;

    public int CourseID { get; set; }
}
public class QuizQuestionDTO{
    public int QuestionID { get; set; }

    public int QuestionBankID { get; set; } 

    public string QuestionText { get; set; } = string.Empty;

    public string Option1 { get; set; } = string.Empty;

    public string Option2 { get; set; } = string.Empty;

    // Option3 and Option4 are optional
    public string? Option3 { get; set; }
    public string? Option4 { get; set; }
}

public class QuizResponseDTO{
    public int ResponseID { get; set; }

    public int AttendanceID { get; set; }  

    public int QuestionID { get; set; }    

    public int SelectedOption { get; set; }
}

public enum AttendanceType
{
    Present,
    Absent,
    Excused
}
