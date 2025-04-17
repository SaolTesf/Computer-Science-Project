using AttendanceSystem.Models;
using AttendanceSystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace AttendanceSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;

        // Correct constructor injection
        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        // Get all students
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudents()
        {
            var students = await _studentService.GetAllStudentsAsync();
            return Ok(students);
        }

        // Get student by UTD ID
        [HttpGet("id/{id}")]
        public async Task<ActionResult<Student>> GetStudentByUTDId(string id)
        {
            var student = await _studentService.GetStudentByUTDIdAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            return Ok(student);
        }

        // Get student by Username
        [HttpGet("username/{username}")]
        public async Task<ActionResult<Student>> GetStudentByUsername(string username)
        {
            var student = await _studentService.GetStudentByUsernameAsync(username);
            if (student == null)
            {
                return NotFound();
            }
            return Ok(student);
        }

        // Add a new student
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

        // Update an existing student
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
        public async Task<IActionResult> DeleteStudentByUTDId(string id)
        {
            //Get the student before deleting
            var student = await _studentService.GetStudentByUTDIdAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            var result = await _studentService.DeleteStudentByUTDIdAsync(id);
            if (!result)
            {
                return StatusCode(500, "Error deleting student.");
            }

            //Return a message with the student's name
            return Ok($"{student.FirstName} {student.LastName} has been removed.");
        }

    }
}