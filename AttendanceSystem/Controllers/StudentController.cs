using AttendanceSystem.Models;
using AttendanceSystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace AttendanceSystem.Controllers {
  [Route("api/[controller]")]
  [ApiController]
  public class StudentController(IStudentService studentService) : ControllerBase {
    private readonly IStudentService _studentService = studentService;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Student>>> GetStudents()
    {
        var students = await _studentService.GetAllStudentsAsync();
        return Ok(students);
    }

        //Get student by UTD ID
        [HttpGet("id/{id}")]
        public async Task<ActionResult<Student>> GetStudentByUTDId(String id)
        {
            var student = await _studentService.GetStudentByUTDIdAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            return Ok(student);
        }

        //Get student by Username
        [HttpGet("username/{username}")]
        public async Task<ActionResult<Student>> GetStudentByUsername(String username)
        {
            var student = await _studentService.GetStudentByUsernameAsync(username);
            if (student == null)
            {
                return NotFound();
            }
            return Ok(student);
        }

        //Add a new student
        [HttpPost]
        public async Task<ActionResult<Student>> AddStudent(Student student)
        {
            var result = await _studentService.AddStudentAsync(student);
            if (!result)
            {
                return BadRequest();
            }
            return CreatedAtAction(nameof(GetStudentByUTDId), new { id = student.UTDID }, student);
        }

        //Update an existing student
        [HttpPut]
        public async Task<IActionResult> UpdateStudent(Student student)
        {
            var result = await _studentService.UpdateStudentAsync(student);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }

        //Delete student by UTD ID
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudentByUTDId(String id)
        {
            var result = await _studentService.DeleteStudentByUTDIdAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }

        //Check if student exists by UTD ID
        [HttpGet("exists/id/{id}")]
        public async Task<IActionResult> StudentExistsByUTDId(String id)
        {
            var exists = await _studentService.StudentExistsByUTDIdAsync(id);
            if (!exists)
            {
                return NotFound();
            }
            return Ok(new { message = "Student exists." });
        }
    }
}
