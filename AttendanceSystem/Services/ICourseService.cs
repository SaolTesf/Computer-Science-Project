using AttendanceSystem.Models;

using AttendanceSystem.Data.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;
using AttendanceSystem.Models.DTOs;

namespace AttendanceSystem.Services
{
    public interface ICourseService
    {
        Task<List<CourseDTO>> GetAllCoursesAsync();          // Retrieve all courses
        Task<CourseDTO?> GetCourseByNumberAsync(string courseNumber); // Retrieve course by CourseNumber
        Task CreateCourseAsync(Course course);              // Create a new course
        Task UpdateCourseAsync(Course course);              // Update an existing course
        Task DeleteCourseAsync(string courseNumber);        // Delete a course by its CourseNumber
    }
}
