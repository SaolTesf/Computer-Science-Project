using AttendanceSystem.Models;

using AttendanceSystem.Data.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AttendanceSystem.Services
{
    public interface IQuizResponseService
    {
        Task<List<QuizResponse>> GetAllResponsesAsync();          // Get all quiz responses
        Task<QuizResponse?> GetResponseByIdAsync(int responseId);    // Get a response by ID
        Task CreateResponseAsync(QuizResponse response);             // Create a new quiz response
        Task UpdateResponseAsync(QuizResponse response);             // Update an existing quiz response
        Task DeleteResponseAsync(int responseId);                    // Delete a quiz response by ID
    }
}
