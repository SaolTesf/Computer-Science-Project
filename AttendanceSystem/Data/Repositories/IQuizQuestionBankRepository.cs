using AttendanceSystem.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AttendanceSystem.Data.Repositories
{
    // Interface defining CRUD operations for QuizQuestionBank.
    public interface IQuizQuestionBankRepository
    {
        Task<IEnumerable<QuizQuestionBank>> GetAllAsync(); // Retrieve all quiz banks.
        Task<QuizQuestionBank?> GetByIdAsync(int bankId); // Retrieve a quiz bank by its ID.
        Task AddAsync(QuizQuestionBank bank); // Add a new quiz bank.
        Task UpdateAsync(QuizQuestionBank bank); // Update an existing quiz bank.
        Task DeleteAsync(QuizQuestionBank bank); // Delete a quiz bank.
    }
}
