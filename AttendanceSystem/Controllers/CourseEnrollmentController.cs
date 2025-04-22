using AttendanceSystem.Models;
using AttendanceSystem.Services;
using AttendanceShared.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AttendanceSystem.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class CourseEnrollmentController : ControllerBase
  {
    private readonly ICourseEnrollmentService _service;
    public CourseEnrollmentController(ICourseEnrollmentService service) => _service = service;

    // GET: api/courseenrollment/course/{courseNumber}
    [HttpGet("course/{courseNumber}")]
    public async Task<ActionResult<List<CourseEnrollmentDetailDTO>>> GetEnrollmentsByCourse(string courseNumber)
    {
      var enrollments = await _service.GetEnrollmentsByCourseAsync(courseNumber);
      var dto = enrollments.Select(e => new CourseEnrollmentDetailDTO {
        EnrollmentID = e.EnrollmentID,
        Student = new StudentDTO {
          UTDID = e.Student!.UTDID,
          FirstName = e.Student.FirstName,
          LastName = e.Student.LastName,
          Username = e.Student.Username
        }
      }).ToList();
      return Ok(dto);
    }

    // POST: api/courseenrollment
    [HttpPost]
    public async Task<ActionResult> EnrollStudent([FromBody] CourseEnrollmentDTO enrollmentDto)
    {
      var enrollment = new CourseEnrollment {
        CourseNumber = enrollmentDto.CourseNumber,
        UTDID = enrollmentDto.UTDID
      };
      await _service.EnrollStudentAsync(enrollment);
      return CreatedAtAction(nameof(GetEnrollmentsByCourse), new { courseNumber = enrollmentDto.CourseNumber }, null);
    }

    // DELETE: api/courseenrollment/{enrollmentId}
    [HttpDelete("{enrollmentId}")]
    public async Task<ActionResult> UnenrollStudent(int enrollmentId)
    {
      await _service.UnenrollStudentAsync(enrollmentId);
      return NoContent();
    }
  }
}