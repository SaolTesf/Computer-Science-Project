using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AttendanceSystem.Data.Repositories;
using AttendanceSystem.Models;
using AttendanceSystem.Models.DTOs;
using Microsoft.IdentityModel.Tokens;

namespace AttendanceSystem.Services;

public class AuthService : IAuthService {
  private readonly IProfessorRepository _professorRepository;
  private readonly IConfiguration _configuration;
  public async Task<AuthResponseDTO?> RegisterAsync(RegisterDTO registerDTO) {

    // Check if username or email already exist
    if(await _professorRepository.ProfessorExistsByUsernameAsync(registerDTO.Username) || await _professorRepository.ProfessorExistsByEmailAsync(registerDTO.Email)){
      return null;
    }

    // create new professor
    var professor = new Professor {
      ID = registerDTO.ID,
      FirstName = registerDTO.FirstName,
      LastName = registerDTO.LastName,
      Username = registerDTO.Username,
      Email = registerDTO.Email,
      PasswordHash = BCrypt.Net.BCrypt.HashPassword(registerDTO.Password)
    };
    await _professorRepository.AddProfessorAsync(professor);

    // create new token by calling function we make
    var token = GenerateJwtToken(professor);

    return new AuthResponseDTO {
      Token = token,
      User = professor
    };
  }

  public async Task<AuthResponseDTO?> LoginAsync(LoginDTO loginDTO) {
    var professor = await _professorRepository.GetProfessorByUsernameAsync(loginDTO.Username);
    if(professor == null || !BCrypt.Net.BCrypt.Verify(loginDTO.Password, professor.PasswordHash)) {
      return null;
    }

    var token = GenerateJwtToken(professor);

    return new AuthResponseDTO {
      Token = token,
      User = professor
    };
  }

  public string GenerateJwtToken(Professor professor) {
    var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"] ?? throw new InvalidOperationException("JWT ket not configured"));
    var tokenHandler = new JwtSecurityTokenHandler();
    var tokenDescriptor = new SecurityTokenDescriptor {
      Subject = new ClaimsIdentity([
        new Claim(ClaimTypes.NameIdentifier, professor.ID),
        new Claim(ClaimTypes.Name, professor.Username),
        new Claim(ClaimTypes.Email, professor.Email),
        new Claim("full_name", $"{professor.FirstName} {professor.LastName}")
      ]),
      Expires = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["Jwt.ExpirationInMinutes"] ?? "60")),
      Issuer = _configuration["Jwt:Issuer"],
      Audience = _configuration["Jwt:Audience"],
      SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
    };

    var token = tokenHandler.CreateToken(tokenDescriptor);
    return tokenHandler.WriteToken(token);
  }
}