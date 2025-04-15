using AttendanceSystem.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AttendanceSystem.Data.Repositories
{
    // Interface defining CRUD operations for QuizResponse.
    public interface IQuizResponseRepository
    {
        Task<IEnumerable<QuizResponse>> GetAllResponsesAsync(); // Retrieve all quiz responses.
        Task<QuizResponse?> GetResponseByIdAsync(int responseId); // Retrieve a quiz response by ID.
        Task AddResponseAsync(QuizResponse response); // Add a new quiz response.
        Task UpdateResponseAsync(QuizResponse response); // Update an existing quiz response.
        Task DeleteResponseAsync(QuizResponse response); // Delete a quiz response.
    }
}
