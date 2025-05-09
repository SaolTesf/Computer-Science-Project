using AttendanceSystem.Models;

using AttendanceSystem.Data.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;
// Dinagaran Senthilkumar
// Interface defining CRUD operations for QuizQuestion.
namespace AttendanceSystem.Services
{
    public interface IQuizQuestionService
    {
        Task<List<QuizQuestion>> GetAllQuestionsAsync();         // Get all quiz questions
        Task<QuizQuestion?> GetQuestionByIdAsync(int questionId);  // Get a question by ID
        Task CreateQuestionAsync(QuizQuestion question);           // Create a new quiz question
        Task UpdateQuestionAsync(QuizQuestion question);           // Update an existing quiz question
        Task DeleteQuestionAsync(int questionId);   // Delete a quiz question by ID

        Task<List<QuizQuestion>> GetQuestionsByBankIdAsync(int bankId);
        Task<int?> GetQuestionIdByTextAsync(string questionText);
    }
}
