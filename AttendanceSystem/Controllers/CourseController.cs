using AttendanceSystem.Models;
using AttendanceSystem.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        // GET: api/course/{courseNumber}
        [HttpGet("{courseNumber}")]
        public async Task<ActionResult<Course>> GetCourseByNumber(string courseNumber)
        {
            var course = await _courseService.GetCourseByNumberAsync(courseNumber);
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
        public async Task<ActionResult> CreateCourse([FromBody] Course course) {
            if (course == null)
                return BadRequest();
            await _courseService.CreateCourseAsync(course);
            return CreatedAtAction(nameof(GetCourseByNumber), new { courseNumber = course.CourseNumber }, course);
        }

        // PUT: api/course/{courseNumber}
        [HttpPut("{courseNumber}")]
        public async Task<ActionResult> UpdateCourse(string courseNumber, [FromBody] Course course)
        {
            if (courseNumber != course.CourseNumber)
                return BadRequest("CourseNumber mismatch.");
            await _courseService.UpdateCourseAsync(course);
            return NoContent();
        }

        // DELETE: api/course/{courseNumber}
        [HttpDelete("{courseNumber}")]
        public async Task<ActionResult> DeleteCourse(string courseNumber)
        {
            await _courseService.DeleteCourseAsync(courseNumber);
            return NoContent();
        }
    }
}
