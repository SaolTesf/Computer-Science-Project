using AttendanceSystem.Models.DTOs;
using AttendanceSystem.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace AttendanceSystem.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase {
  private readonly IAuthService _authService;

  [HttpPost("register")]
  public async Task<ActionResult<AuthResponseDTO>> Register(RegisterDTO registerDTO) {
    var response = await _authService.RegisterAsync(registerDTO);
    if (response == null) {
      return BadRequest("Username or email already exists");
    }

    return Ok(response);
  }

  [HttpPost("login")]
  public async Task<ActionResult<AuthResponseDTO>> Login(LoginDTO loginDTO) {
    var response = await _authService.LoginAsync(loginDTO);
    if(response == null) {
      return Unauthorized("Invalud username or password");
    }

    return Ok(response);
  }
}