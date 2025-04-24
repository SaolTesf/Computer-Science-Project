using AttendanceSystem.Models; // Import Course model
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AttendanceSystem.Data.Repositories
{
    // Interface defining CRUD operations for Course.
    public interface ICourseRepository
    {
        Task<IEnumerable<Course>> GetAllCoursesAsync(); // Retrieve all courses.
        Task<Course?> GetByCourseNumberAsync(string courseNumber); // Get course by primary key (CourseNumber).
        Task AddCourseAsync(Course course); // Add a new course.
        Task UpdateCourseAsync(Course course); // Update an existing course.
        Task DeleteCourseAsync(Course course); // Delete a course.
    }
}