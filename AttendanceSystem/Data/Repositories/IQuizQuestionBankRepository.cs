using AttendanceSystem.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AttendanceSystem.Data.Repositories
{
    // Interface defining CRUD operations for QuizQuestionBank.
    public interface IQuizQuestionBankRepository
    {
        Task<IEnumerable<QuizQuestionBank>> GetAllBanksAsync(); // Retrieve all quiz banks.
        Task<QuizQuestionBank?> GetBankByIdAsync(int bankId); // Retrieve a quiz bank by its ID.
        Task AddBankAsync(QuizQuestionBank bank); // Add a new quiz bank.
        Task UpdateBankAsync(QuizQuestionBank bank); // Update an existing quiz bank.
        Task DeleteBankAsync(QuizQuestionBank bank); // Delete a quiz bank.
    }
}
