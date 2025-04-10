using AttendanceSystem.Data;
using AttendanceSystem.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AttendanceSystem.Data.Repositories
{
    // Implements IQuizQuestionRepository using EF Core.
    public class QuizQuestionRepository : IQuizQuestionRepository
    {
        private readonly AppDbContext _context;
        public QuizQuestionRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<QuizQuestion>> GetAllAsync()
        {
            return await _context.QuizQuestions.ToListAsync();
        }

        public async Task<QuizQuestion?> GetByIdAsync(int questionId)
        {
            return await _context.QuizQuestions.FindAsync(questionId);
        }

        public async Task AddAsync(QuizQuestion question)
        {
            await _context.QuizQuestions.AddAsync(question);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(QuizQuestion question)
        {
            _context.QuizQuestions.Update(question);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(QuizQuestion question)
        {
            _context.QuizQuestions.Remove(question);
            await _context.SaveChangesAsync();
        }
    }
}
