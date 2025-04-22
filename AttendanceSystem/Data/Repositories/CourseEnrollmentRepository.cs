/*
Saol Tesfaghebriel
CourseEnrollmentRepository interface that defines methods for managing course enrollments in the attendance system.
*/


namespace AttendanceSystem.Data.Repositories
{
  using AttendanceSystem.Data;
  using AttendanceSystem.Models;
  using Microsoft.EntityFrameworkCore;
  using System.Collections.Generic;
  using System.Linq;
  using System.Threading.Tasks;

  // EF Core implementation of ICourseEnrollmentRepository
  public class CourseEnrollmentRepository : ICourseEnrollmentRepository
  {
    private readonly AppDbContext _context;
    public CourseEnrollmentRepository(AppDbContext context) => _context = context;

    public async Task<IEnumerable<CourseEnrollment>> GetByCourseNumberAsync(string courseNumber)
      => await _context.CourseEnrollments
                        .Include(e => e.Student)
                        .Where(e => e.CourseNumber == courseNumber)
                        .ToListAsync();

    public async Task AddAsync(CourseEnrollment enrollment)
    {
      await _context.CourseEnrollments.AddAsync(enrollment);
      await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int enrollmentId)
    {
      var entity = await _context.CourseEnrollments.FindAsync(enrollmentId);
      if (entity != null)
      {
        _context.CourseEnrollments.Remove(entity);
        await _context.SaveChangesAsync();
      }
    }
  }
}