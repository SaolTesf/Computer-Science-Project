using AttendanceSystem.Models; // Import Course model
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AttendanceSystem.Data.Repositories
{
    // Interface defining CRUD operations for Course.
    public interface ICourseRepository
    {
        Task<IEnumerable<Course>> GetAllAsync(); // Retrieve all courses.
        Task<Course?> GetByCourseNumberAsync(string courseNumber); // Get course by primary key (CourseNumber).
        Task AddAsync(Course course); // Add a new course.
        Task UpdateAsync(Course course); // Update an existing course.
        Task DeleteAsync(Course course); // Delete a course.
    }
}
