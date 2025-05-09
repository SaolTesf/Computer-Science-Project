using AttendanceSystem.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
// Dinagaran Senthilkumar

// Interface defining CRUD operations for QuizQuestionBank.
namespace AttendanceSystem.Data.Repositories
{
   
    public interface IQuizQuestionBankRepository
    {
        Task<IEnumerable<QuizQuestionBank>> GetAllBanksAsync(); // Retrieve all quiz banks.
        Task<QuizQuestionBank?> GetBankByIdAsync(int bankId); // Retrieve a quiz bank by its ID.
        Task AddBankAsync(QuizQuestionBank bank); // Add a new quiz bank.
        Task UpdateBankAsync(QuizQuestionBank bank); // Update an existing quiz bank.
        Task DeleteBankAsync(QuizQuestionBank bank); // Delete a quiz bank.
        Task<List<string>> GetAllBankNamesAsync();
        Task<int?> GetQuestionBankIdByNameAsync(string bankName);
        Task<List<QuizQuestionBank>> GetBanksByCourseIdAsync(int courseId);
    }
}
