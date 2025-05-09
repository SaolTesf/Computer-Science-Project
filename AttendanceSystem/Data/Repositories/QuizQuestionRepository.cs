using AttendanceSystem.Data;
using AttendanceSystem.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
// Dinagaran Senthilkumar
// This is basically the QuizQuestionRepository.cs file that implements the IQuizQuestionRepository interface.
namespace AttendanceSystem.Data.Repositories
{
    
    public class QuizQuestionRepository : IQuizQuestionRepository
    {
        private readonly AppDbContext _context;
        public QuizQuestionRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<QuizQuestion>> GetAllQuestionsAsync()
        {
            return await _context.QuizQuestions.ToListAsync();
        }

        public async Task<QuizQuestion?> GetQuestionByIdAsync(int questionId)
        {
            return await _context.QuizQuestions.FindAsync(questionId);
        }

        public async Task AddQuestionAsync(QuizQuestion question)
        {
            await _context.QuizQuestions.AddAsync(question);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateQuestionAsync(QuizQuestion question)
        {
            _context.QuizQuestions.Update(question);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteQuestionAsync(QuizQuestion question)
        {
            _context.QuizQuestions.Remove(question);
            await _context.SaveChangesAsync();
        }
        public async Task<List<QuizQuestion>> GetQuestionsByBankIdAsync(int bankId)
        {
            return await _context.QuizQuestions
                .Where(q => q.QuestionBankID == bankId)
                .ToListAsync();
        }
        public async Task<int?> GetQuestionIdByTextAsync(string questionText)
        {
            var question = await _context.QuizQuestions
                .FirstOrDefaultAsync(q => q.QuestionText.Contains(questionText));
            return question?.QuestionID; 
        }
    }
}
