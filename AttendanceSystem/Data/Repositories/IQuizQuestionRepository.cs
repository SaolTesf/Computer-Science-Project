using AttendanceSystem.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AttendanceSystem.Data.Repositories
{
    // Interface defining CRUD operations for QuizQuestion.
    public interface IQuizQuestionRepository
    {
        Task<IEnumerable<QuizQuestion>> GetAllAsync(); // Retrieve all quiz questions.
        Task<QuizQuestion?> GetByIdAsync(int questionId); // Retrieve a quiz question by ID.
        Task AddAsync(QuizQuestion question); // Add a new quiz question.
        Task UpdateAsync(QuizQuestion question); // Update an existing quiz question.
        Task DeleteAsync(QuizQuestion question); // Delete a quiz question.
    }
}
