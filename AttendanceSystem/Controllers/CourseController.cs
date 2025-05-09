using AttendanceSystem.Models;
using AttendanceSystem.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// Dinagaran Senthilkumar
// CourseControllers.cs file that handles HTTP requests related to course records.  I added the links to help me test it on postman easier

namespace AttendanceSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _courseService;

        public CourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        // GET: api/course
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Course>>> GetAllCourses()
        {
            var courses = await _courseService.GetAllCoursesAsync();
            return Ok(courses);
        }

        // GET: api/course/{courseID}
        [HttpGet("{courseID}")]
        public async Task<ActionResult<Course>> GetCourseByNumber(int courseID)
        {
            var course = await _courseService.GetCourseByIDAsync(courseID);
            if (course == null)
                return NotFound();
            return Ok(course);
        }

        // GET: api/course/professor/{professorId}
        [HttpGet("professor/{professorId}")]
        public async Task<ActionResult<IEnumerable<Course>>> GetCoursesByProfessor(string professorId)
        {
            var allCourses = await _courseService.GetAllCoursesAsync();
            var filtered = allCourses.Where(c => c.ProfessorID == professorId);
            return Ok(filtered);
        }

        // POST: api/course
        [HttpPost]
        public async Task<ActionResult> CreateCourse([FromBody] Course course)
        {
            if (course == null)
                return BadRequest();
            await _courseService.CreateCourseAsync(course);
            return CreatedAtAction(nameof(GetCourseByNumber), new { courseID = course.CourseID }, course);
        }

        // PUT: api/course/{courseID}
        [HttpPut("{courseID}")]
        public async Task<ActionResult> UpdateCourse(int courseID, [FromBody] Course course)
        {
            if (courseID != course.CourseID)
                return BadRequest("CourseID mismatch.");
            await _courseService.UpdateCourseAsync(course);
            return NoContent();
        }

        // DELETE: api/course/{courseID}
        [HttpDelete("{courseID}")]
        public async Task<ActionResult> DeleteCourse(int courseID)
        {
            await _courseService.DeleteCourseAsync(courseID);
            return NoContent();
        }
    }
}