/*
Saol Tesfaghebriel
CourseEnrollmentRepository interface that defines methods for managing course enrollments in the attendance system.
*/


namespace AttendanceSystem.Data.Repositories
{
  using AttendanceSystem.Models;
  using System.Collections.Generic;
  using System.Threading.Tasks;

  public interface ICourseEnrollmentRepository
  {
    Task<IEnumerable<CourseEnrollment>> GetByCourseNumberAsync(string courseNumber);
    Task AddAsync(CourseEnrollment enrollment);
    Task DeleteAsync(int enrollmentId);
  }
}