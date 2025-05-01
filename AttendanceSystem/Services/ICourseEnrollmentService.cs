namespace AttendanceSystem.Services
{
  using AttendanceSystem.Models;
  using System.Collections.Generic;
  using System.Threading.Tasks;

  // Service for managing course enrollments
  public interface ICourseEnrollmentService
  {
    Task<List<CourseEnrollment>> GetEnrollmentsByCourseAsync(int courseID);
    Task EnrollStudentAsync(CourseEnrollment enrollment);
    Task UnenrollStudentAsync(int enrollmentId);
  }
}