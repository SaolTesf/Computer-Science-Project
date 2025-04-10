using AttendanceSystem.Models;
using AttendanceSystem.Data.Repositories; // Assumes ICourseRepository exists
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AttendanceSystem.Services
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courseRepository; // Repository dependency

        public CourseService(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public async Task<List<Course>> GetAllCoursesAsync()
        {
            var courses = await _courseRepository.GetAllAsync();
            return new List<Course>(courses);
        }

        public async Task<Course?> GetCourseByNumberAsync(string courseNumber)
        {
            return await _courseRepository.GetByCourseNumberAsync(courseNumber);
        }

        public async Task CreateCourseAsync(Course course)
        {
            await _courseRepository.AddAsync(course);
        }

        public async Task UpdateCourseAsync(Course course)
        {
            await _courseRepository.UpdateAsync(course);
        }

        public async Task DeleteCourseAsync(string courseNumber)
        {
            var course = await _courseRepository.GetByCourseNumberAsync(courseNumber);
            if (course != null)
            {
                await _courseRepository.DeleteAsync(course);
            }
        }
    }
}
