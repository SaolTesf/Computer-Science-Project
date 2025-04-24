using AttendanceSystem.Data; // For AppDbContext
using AttendanceSystem.Models; // For Course model
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AttendanceSystem.Data.Repositories
{
    // Implements ICourseRepository using EF Core.
    public class CourseRepository : ICourseRepository
    {
        private readonly AppDbContext _context;
        public CourseRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Course>> GetAllCoursesAsync()
        {
            return await _context.Courses.ToListAsync();
        }

        public async Task<Course?> GetByCourseNumberAsync(string courseNumber)
        {
            // CourseNumber is the primary key.
            return await _context.Courses.FindAsync(courseNumber);
        }

        public async Task<IEnumerable<Course>> GetCoursesByProfessorAsync(string professorID)
        {
            return await _context.Courses.ToListAsync();
        }
        
        public async Task AddCourseAsync(Course course)
        {
            await _context.Courses.AddAsync(course);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCourseAsync(Course course)
        {
            _context.Courses.Update(course);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCourseAsync(Course course)
        {
            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
        }
    }
}
