/*
Saol Tesfaghebriel
AuthService interface that defines the methods for user authentication and registration in the attendance system.
*/

using AttendanceSystem.Models;
using AttendanceSystem.Models.DTOs;
using System.IdentityModel.Tokens.Jwt;

namespace AttendanceSystem.Services;

public interface IAuthService {
    Task<AuthResponseDTO?> RegisterAsync(RegisterDTO registerDTO);
    Task<AuthResponseDTO?> LoginAsync(LoginDTO loginDTO);
}