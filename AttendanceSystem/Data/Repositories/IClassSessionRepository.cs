using AttendanceSystem.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AttendanceSystem.Data.Repositories
{
    // Interface defining CRUD operations for ClassSession.
    public interface IClassSessionRepository
    {
        Task<IEnumerable<ClassSession>> GetAllSessionsAsync(); // Retrieve all class sessions.
        Task<ClassSession?> GetSessionByIdAsync(int sessionId); // Get a session by its ID.
        Task AddSessionAsync(ClassSession session); // Add a new session.
        Task UpdateSessionAsync(ClassSession session); // Update an existing session.
        Task DeleteSessionAsync(ClassSession session); // Delete a session.
    }
}
