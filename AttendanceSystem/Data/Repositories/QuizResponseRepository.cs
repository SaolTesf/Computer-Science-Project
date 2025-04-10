using AttendanceSystem.Data;
using AttendanceSystem.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AttendanceSystem.Data.Repositories
{
    // Implements IQuizResponseRepository using EF Core.
    public class QuizResponseRepository : IQuizResponseRepository
    {
        private readonly AppDbContext _context;
        public QuizResponseRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<QuizResponse>> GetAllAsync()
        {
            return await _context.QuizResponses.ToListAsync();
        }

        public async Task<QuizResponse?> GetByIdAsync(int responseId)
        {
            return await _context.QuizResponses.FindAsync(responseId);
        }

        public async Task AddAsync(QuizResponse response)
        {
            await _context.QuizResponses.AddAsync(response);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(QuizResponse response)
        {
            _context.QuizResponses.Update(response);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(QuizResponse response)
        {
            _context.QuizResponses.Remove(response);
            await _context.SaveChangesAsync();
        }
    }
}
