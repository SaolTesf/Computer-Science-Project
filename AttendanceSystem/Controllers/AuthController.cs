/*
Saol Tesfaghebriel
AuthController class that handles user authentication and registration in the attendance system.
*/

using AttendanceSystem.Models.DTOs;
using AttendanceSystem.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace AttendanceSystem.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController(IAuthService authService) : ControllerBase {
    private readonly IAuthService _authService = authService;

    [HttpPost("register")]
    public async Task<ActionResult<AuthResponseDTO>> Register(RegisterDTO registerDTO) {
        var response = await _authService.RegisterAsync(registerDTO);
        if (response != null && response.Token == "")
        {
            return BadRequest("ID, username, or email already exist.");
        } else if (response == null) {
            return BadRequest("Failed to register.");
        }
        return Ok(response);
    }

  [HttpPost("login")]
  public async Task<ActionResult<AuthResponseDTO>> Login(LoginDTO loginDTO) {
    var response = await _authService.LoginAsync(loginDTO);
    if (response == null) {
        return BadRequest("Failed to log in.");
    }

    return Ok(response);
  }
}