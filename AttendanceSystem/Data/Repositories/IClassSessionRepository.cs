using AttendanceSystem.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AttendanceSystem.Data.Repositories
{
    // Interface defining CRUD operations for ClassSession.
    public interface IClassSessionRepository
    {
        Task<IEnumerable<ClassSession>> GetAllSessionsAsync(); // Retrieve all class sessions.
        Task<IEnumerable<ClassSession>> GetByCourseIDAsync(int courseID); // Retrieve class sessions with a specific course ID
        Task<ClassSession?> GetSessionByIdAsync(int sessionId); // Get a session by its ID.
        Task AddSessionAsync(ClassSession session); // Add a new session.
        Task UpdateSessionAsync(ClassSession session); // Update an existing session.
        Task DeleteSessionAsync(ClassSession session); // Delete a session.
        Task<IEnumerable<ClassSession>> GetSessionBySessionDateTimeAsync(DateTime SessionDateTime);

        Task<ClassSession?> GetCurrentSessionAsync(DateTime currentTime);
        Task<ClassSession?> GetByAccessCodeAsync(string accessCode);
    }
}
