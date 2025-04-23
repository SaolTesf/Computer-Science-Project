using AttendanceSystem.Models;

using AttendanceSystem.Data.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AttendanceSystem.Services
{
    public interface IQuizQuestionBankService
    {
        Task<List<QuizQuestionBank>> GetAllBanksAsync();          // Get all quiz banks
        Task<QuizQuestionBank?> GetBankByIdAsync(int bankId);       // Get quiz bank by ID
        Task CreateBankAsync(QuizQuestionBank bank);                // Create a new quiz bank
        Task UpdateBankAsync(QuizQuestionBank bank);                // Update an existing quiz bank
        Task DeleteBankAsync(int bankId);                           // Delete a quiz bank by ID
        Task<IEnumerable<string>> GetAllBankNamesAsync();
    }
}
