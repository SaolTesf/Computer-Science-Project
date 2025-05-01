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

        public async Task<IEnumerable<QuizQuestionBank>> GetAllBanksAsync()
        {
            return await _context.QuizQuestionBanks.ToListAsync();
        }

        public async Task<QuizQuestionBank?> GetBankByIdAsync(int bankId)
        {
            return await _context.QuizQuestionBanks.FindAsync(bankId);
        }

        public async Task AddBankAsync(QuizQuestionBank bank)
        {
            await _context.QuizQuestionBanks.AddAsync(bank);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateBankAsync(QuizQuestionBank bank)
        {
            _context.QuizQuestionBanks.Update(bank);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteBankAsync(QuizQuestionBank bank)
        {
            _context.QuizQuestionBanks.Remove(bank);
            await _context.SaveChangesAsync();
        }

        public async Task<List<string>> GetAllBankNamesAsync()
        {
            return await _context.QuizQuestionBanks
                                 .Select(q => q.BankName)
                                 .ToListAsync();
        }

        public async Task<int?> GetQuestionBankIdByNameAsync(string bankName)
        {
            var bank = await _context.QuizQuestionBanks .FirstOrDefaultAsync(qb => qb.BankName == bankName);

            return bank?.QuestionBankID;
        }

        public async Task<List<QuizQuestionBank>> GetBanksByCourseIdAsync(int courseId)
        {
            return await _context.QuizQuestionBanks
                                 .Where(b => b.CourseID == courseId)
                                 .ToListAsync();
        }

    }
}
