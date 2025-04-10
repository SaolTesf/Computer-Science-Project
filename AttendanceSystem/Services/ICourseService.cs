using AttendanceSystem.Models;

using AttendanceSystem.Data.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AttendanceSystem.Services
{
    public interface ICourseService
    {
        Task<List<Course>> GetAllCoursesAsync();          // Retrieve all courses
        Task<Course?> GetCourseByNumberAsync(string courseNumber); // Retrieve course by CourseNumber
        Task CreateCourseAsync(Course course);              // Create a new course
        Task UpdateCourseAsync(Course course);              // Update an existing course
        Task DeleteCourseAsync(string courseNumber);        // Delete a course by its CourseNumber
    }
}
