using AttendanceSystem.Data;
using AttendanceSystem.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
// // Dinagaran Senthilkumar
// // This is basically the QuizResponseRepository.cs file that implements the IQuizResponseRepository interface.
namespace AttendanceSystem.Data.Repositories
{
    
    public class QuizResponseRepository : IQuizResponseRepository
    {
        private readonly AppDbContext _context;
        public QuizResponseRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<QuizResponse>> GetAllResponsesAsync()
        {
            return await _context.QuizResponses.ToListAsync();
        }

        public async Task<QuizResponse?> GetResponseByIdAsync(int responseId)
        {
            return await _context.QuizResponses.FindAsync(responseId);
        }

        public async Task AddResponseAsync(QuizResponse response)
        {
            await _context.QuizResponses.AddAsync(response);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateResponseAsync(QuizResponse response)
        {
            _context.QuizResponses.Update(response);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteResponseAsync(QuizResponse response)
        {
            _context.QuizResponses.Remove(response);
            await _context.SaveChangesAsync();
        }
    }
}
