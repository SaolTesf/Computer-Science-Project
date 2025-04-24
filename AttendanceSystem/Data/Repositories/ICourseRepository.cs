using AttendanceSystem.Models; // Import Course model
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AttendanceSystem.Data.Repositories
{
    // Interface defining CRUD operations for Course.
    public interface ICourseRepository
    {
        Task<IEnumerable<Course>> GetAllCoursesAsync(); // Retrieve all courses.
        Task<Course?> GetCourseByIDAsync(int courseID); // Get course by primary key (CourseID).
        Task AddCourseAsync(Course course); // Add a new course.
        Task UpdateCourseAsync(Course course); // Update an existing course.
        Task DeleteCourseAsync(Course course); // Delete a course.
    }
}