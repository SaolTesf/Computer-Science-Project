using AttendanceSystem.Data;
using AttendanceSystem.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AttendanceSystem.Data.Repositories
{
    public class SessionQuestionRepository : ISessionQuestionRepository
    {
        private readonly AppDbContext _context;
        public SessionQuestionRepository(AppDbContext context) => _context = context;

        public async Task<IEnumerable<SessionQuestion>> GetAllAsync() =>
            await _context.SessionQuestions.ToListAsync();

        public async Task<SessionQuestion?> GetByIdAsync(int id) =>
            await _context.SessionQuestions.FindAsync(id);

        public async Task<IEnumerable<SessionQuestion>> GetBySessionIdAsync(int sessionId) =>
            await _context.SessionQuestions
                .Where(sq => sq.SessionID == sessionId)
                .ToListAsync();

        public async Task<IEnumerable<SessionQuestion>> GetByQuestionIdAsync(int questionId) =>
            await _context.SessionQuestions
                .Where(sq => sq.QuestionID == questionId)
                .ToListAsync();

        public async Task AddAsync(SessionQuestion entity) {
            await _context.SessionQuestions.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(SessionQuestion entity) {
            _context.SessionQuestions.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(SessionQuestion entity) {
            _context.SessionQuestions.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}