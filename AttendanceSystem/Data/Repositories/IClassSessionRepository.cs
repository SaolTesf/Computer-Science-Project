using AttendanceSystem.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AttendanceSystem.Data.Repositories
{
    // Interface defining CRUD operations for ClassSession.
    public interface IClassSessionRepository
    {
        Task<IEnumerable<ClassSession>> GetAllAsync(); // Retrieve all class sessions.
        Task<ClassSession?> GetByIdAsync(int sessionId); // Get a session by its ID.
        Task AddAsync(ClassSession session); // Add a new session.
        Task UpdateAsync(ClassSession session); // Update an existing session.
        Task DeleteAsync(ClassSession session); // Delete a session.
    }
}
