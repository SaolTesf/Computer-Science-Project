using AttendanceSystem.Data;
using AttendanceSystem.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace AttendanceSystem.Data.Repositories
{
    // Implements IClassSessionRepository using EF Core.
    public class ClassSessionRepository : IClassSessionRepository
    {
        private readonly AppDbContext _context;
        public ClassSessionRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ClassSession>> GetAllSessionsAsync()
        {
            return await _context.ClassSessions.ToListAsync();
        }

        public async Task<ClassSession?> GetSessionByIdAsync(int sessionId)
        {
            return await _context.ClassSessions.FindAsync(sessionId);
        }
        public async Task<IEnumerable<ClassSession>> GetByCourseIDAsync(int courseID)
        => await _context.ClassSessions
                    .Where(e => e.CourseID == courseID)
                    .ToListAsync();

        public async Task AddSessionAsync(ClassSession session)
        {
            await _context.ClassSessions.AddAsync(session);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateSessionAsync(ClassSession session)
        {
            _context.ClassSessions.Update(session);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteSessionAsync(ClassSession session)
        {
            _context.ClassSessions.Remove(session);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ClassSession>> GetSessionBySessionDateTimeAsync(DateTime SessionDateTime)
        => await _context.ClassSessions
                    .Where(e => e.SessionDateTime == SessionDateTime)
                    .ToListAsync();
    }
}
