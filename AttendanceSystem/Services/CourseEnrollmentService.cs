/*
Saol Tesfaghebriel
CourseEnrollmentRepository interface that defines methods for managing course enrollments in the attendance system.
*/


namespace AttendanceSystem.Services
{
  using AttendanceSystem.Data.Repositories;
  using AttendanceSystem.Models;
  using System.Collections.Generic;
  using System.Threading.Tasks;

  // Implements ICourseEnrollmentService
  public class CourseEnrollmentService : ICourseEnrollmentService
  {
    private readonly ICourseEnrollmentRepository _courseEnrollmentRepository;
    public CourseEnrollmentService(ICourseEnrollmentRepository repo) => _courseEnrollmentRepository = repo;

    public async Task<List<CourseEnrollment>> GetEnrollmentsByCourseAsync(string courseNumber)
      => new List<CourseEnrollment>(await _courseEnrollmentRepository.GetByCourseNumberAsync(courseNumber));

    public async Task EnrollStudentAsync(CourseEnrollment enrollment)
      => await _courseEnrollmentRepository.AddAsync(enrollment);

    public async Task UnenrollStudentAsync(int enrollmentId)
      => await _courseEnrollmentRepository.DeleteAsync(enrollmentId);
  }
}