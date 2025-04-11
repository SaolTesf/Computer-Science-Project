/*
Saol Tesfaghebriel
AuthDTOs class that contains data transfer objects for user authentication and registration in the attendance system.
*/

namespace AttendanceSystem.Models.DTOs;

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
  public Professor User { get; set; } = null!;

}