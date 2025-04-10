using AttendanceSystem.Data;
using AttendanceSystem.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AttendanceSystem.Data.Repositories
{
    // Implements IQuizQuestionBankRepository using EF Core.
    public class QuizQuestionBankRepository : IQuizQuestionBankRepository
    {
        private readonly AppDbContext _context;
        public QuizQuestionBankRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<QuizQuestionBank>> GetAllAsync()
        {
            return await _context.QuizQuestionBanks.ToListAsync();
        }

        public async Task<QuizQuestionBank?> GetByIdAsync(int bankId)
        {
            return await _context.QuizQuestionBanks.FindAsync(bankId);
        }

        public async Task AddAsync(QuizQuestionBank bank)
        {
            await _context.QuizQuestionBanks.AddAsync(bank);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(QuizQuestionBank bank)
        {
            _context.QuizQuestionBanks.Update(bank);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(QuizQuestionBank bank)
        {
            _context.QuizQuestionBanks.Remove(bank);
            await _context.SaveChangesAsync();
        }
    }
}
