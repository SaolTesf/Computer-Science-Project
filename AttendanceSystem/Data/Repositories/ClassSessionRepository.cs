using AttendanceSystem.Data;
using AttendanceSystem.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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

        public async Task<IEnumerable<ClassSession>> GetAllAsync()
        {
            return await _context.ClassSessions.ToListAsync();
        }

        public async Task<ClassSession?> GetByIdAsync(int sessionId)
        {
            return await _context.ClassSessions.FindAsync(sessionId);
        }

        public async Task AddAsync(ClassSession session)
        {
            await _context.ClassSessions.AddAsync(session);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ClassSession session)
        {
            _context.ClassSessions.Update(session);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(ClassSession session)
        {
            _context.ClassSessions.Remove(session);
            await _context.SaveChangesAsync();
        }
    }
}
