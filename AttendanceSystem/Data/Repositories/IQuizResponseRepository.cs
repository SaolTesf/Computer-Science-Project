using AttendanceSystem.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AttendanceSystem.Data.Repositories
{
    // Interface defining CRUD operations for QuizResponse.
    public interface IQuizResponseRepository
    {
        Task<IEnumerable<QuizResponse>> GetAllAsync(); // Retrieve all quiz responses.
        Task<QuizResponse?> GetByIdAsync(int responseId); // Retrieve a quiz response by ID.
        Task AddAsync(QuizResponse response); // Add a new quiz response.
        Task UpdateAsync(QuizResponse response); // Update an existing quiz response.
        Task DeleteAsync(QuizResponse response); // Delete a quiz response.
    }
}
