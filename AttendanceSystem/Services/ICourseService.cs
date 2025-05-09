using AttendanceSystem.Models;

using AttendanceSystem.Data.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;
using AttendanceSystem.Models.DTOs;
// Dinagaran Senthilkumar
// Interface defining CRUD operations for Course.
namespace AttendanceSystem.Services
{
    public interface ICourseService
    {
        Task<List<CourseDTO>> GetAllCoursesAsync();          // Retrieve all courses
        Task<CourseDTO?> GetCourseByIDAsync(int courseID); // Retrieve course by CourseID
        Task CreateCourseAsync(Course course);              // Create a new course
        Task UpdateCourseAsync(Course course);              // Update an existing course
        Task DeleteCourseAsync(int courseID);        // Delete a course by its CourseID
    }
}
