using AttendanceSystem.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AttendanceSystem.Data.Repositories
{
    // Interface defining CRUD operations for QuizQuestion.
    public interface IQuizQuestionRepository
    {
        Task<IEnumerable<QuizQuestion>> GetAllQuestionsAsync(); // Retrieve all quiz questions.
        Task<QuizQuestion?> GetQuestionByIdAsync(int questionId); // Retrieve a quiz question by ID.
        Task AddQuestionAsync(QuizQuestion question); // Add a new quiz question.
        Task UpdateQuestionAsync(QuizQuestion question); // Update an existing quiz question.
        Task DeleteQuestionAsync(QuizQuestion question); // Delete a quiz question.

        Task<List<QuizQuestion>> GetQuestionsByBankIdAsync(int bankId);

    }
}
