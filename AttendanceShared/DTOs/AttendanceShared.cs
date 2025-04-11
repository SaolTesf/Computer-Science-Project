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
  public string Username { get; set; } = null!;
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
