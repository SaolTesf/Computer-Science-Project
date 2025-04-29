using AttendanceSystem.Data;
using AttendanceSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace AttendanceSystem.Repositories
{
    public class SelectedQuizQuestionRepository : ISelectedQuizQuestionRepository
    {
        private readonly AppDbContext _context;

        public SelectedQuizQuestionRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddSelectedQuestionsAsync(List<SelectedQuizQuestion> questions)
        {
            await _context.SelectedQuizQuestions.AddRangeAsync(questions);
            await _context.SaveChangesAsync();
        }

        public async Task<List<SelectedQuizQuestion>> GetAllSelectedQuestionsAsync()
        {
            return await _context.SelectedQuizQuestions.ToListAsync();
        }

        public async Task ClearSelectedQuestionsAsync()
        {
            var all = await _context.SelectedQuizQuestions.ToListAsync();
            _context.SelectedQuizQuestions.RemoveRange(all);
            await _context.SaveChangesAsync();
        }
    }
}
