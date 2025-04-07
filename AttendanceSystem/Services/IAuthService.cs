using AttendanceSystem.Models;
using AttendanceSystem.Models.DTOs;

namespace AttendanceSystem.Services;

public interface IAuthService {
    Task<AuthResponseDTO?> RegisterAsync(RegisterDTO registerDTO);
    Task<AuthResponseDTO?> LoginAsync(LoginDTO loginDTO);
}