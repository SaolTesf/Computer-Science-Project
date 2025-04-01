using AttendanceSystem.Models;
using AttendanceSystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace AttendanceSystem.Controllers {
  [Route("api/[controller]")]
  [ApiController]
  public class ProfessorController(IProfessorService professorService) : ControllerBase {
    private readonly IProfessorService _professorService = professorService;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Professor>>> GetProfessors(){
      var professor = await _professorService.GetAllProfessorsAsync();
      return Ok(professor);
    }

    [HttpGet("id/{id}")]
    public async Task<ActionResult<Professor>> GetProfessorById(String id){
      var professor = await _professorService.GetProfessorByIdAsync(id);
      if(professor == null){
        return NotFound();
      }

      return Ok(professor);
    }

    [HttpGet("username/{username}")]
    public async Task<ActionResult<Professor>> GetProfessorByUsername(String username){
      var professor = await _professorService.GetProfessorByEmailAsync(username);
      if(professor == null){
        return NotFound();
      }

      return Ok(professor);
    }

    [HttpGet("email/{email}")]
    public async Task<ActionResult<Professor>> GetProfessorByEmail(String email){
      var professor = await _professorService.GetProfessorByEmailAsync(email);
      if(professor == null){
        return NotFound();
      }

      return Ok(professor);
    }
    
    [HttpPost]
    public async Task<ActionResult<Professor>> AddProfessor(Professor professor){
      var result = await _professorService.AddProfessorAsync(professor);
      if(!result){
        return BadRequest();
      }

      return CreatedAtAction(nameof(GetProfessorById), new { id = professor.ID }, professor);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateProfessor(Professor professor){
      var result = await _professorService.UpdateProfessorAsync(professor);

      if(!result){
        return NotFound();
      }

      return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProfessorById(String id){
      var result = await _professorService.DeleteProfessorByIdAsync(id);

      if(!result){
        return NotFound();
      }

      return NoContent();
    }

    [HttpGet("exists/id/{id}")]
    public async Task<IActionResult> ProfessorExistsById(String id){
      var exists = await _professorService.ProfessorExistsByIdAsync(id);
      if(!exists){
        return NotFound();
      }

      return Ok(new { message = "Professor exists." });
    }

    [HttpGet("exists/username/{username}")]
    public async Task<IActionResult> ProfessorExistsByUsername(String username){
      var exists = await _professorService.ProfessorExistsByUsernameAsync(username);
      if(!exists){
        return NotFound();
      }

      return Ok(new { message = "Professor exists." });
    }

    [HttpGet("exists/email/{email}")]
    public async Task<IActionResult> ProfessorExistsByEmail(String email){
      var exist = await _professorService.ProfessorExistsByEmailAsync(email);

      if(!exist){
        return NotFound();
      }

      return Ok(new { message = "Professor exists."});
    }
  }
}