/*
Saol Tesfaghebriel
AuthController class that handles user authentication and registration in the attendance system.
*/

using AttendanceSystem.Models;
using AttendanceSystem.Models.DTOs;
using AttendanceSystem.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AttendanceSystem.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController(IAuthService authService) : ControllerBase {
  private readonly IAuthService _authService = authService;

  [HttpPost("register")]
  public async Task<ActionResult<AuthResponseDTO>> Register(RegisterDTO registerDTO) {
    var response = await _authService.RegisterAsync(registerDTO);
    if (response == null) {
      return BadRequest("ID, Username, or email already exists");
    }

    return Ok(response);
  }

  [HttpPost("login")]
  public async Task<ActionResult<AuthResponseDTO>> Login(LoginDTO loginDTO) {
    var response = await _authService.LoginAsync(loginDTO);
    if(response == null) {
      return Unauthorized("Invalid Identifier or password");
    }

    return Ok(response);
  }
}