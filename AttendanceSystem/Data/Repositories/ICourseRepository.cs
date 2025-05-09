using AttendanceSystem.Models; // Import Course model
using System.Collections.Generic;
using System.Threading.Tasks;
//Dinagaran Senthilkumar
// Interface defining CRUD operations for QuizQuestionBank.
namespace AttendanceSystem.Data.Repositories
{

  
    public interface ICourseRepository
    {
        Task<IEnumerable<Course>> GetAllCoursesAsync(); // Retrieve all courses.
        Task<Course?> GetCourseByIDAsync(int courseID); // Get course by primary key (CourseID).
        Task AddCourseAsync(Course course); // Add a new course.
        Task UpdateCourseAsync(Course course); // Update an existing course.
        Task DeleteCourseAsync(Course course); // Delete a course.
    }
}