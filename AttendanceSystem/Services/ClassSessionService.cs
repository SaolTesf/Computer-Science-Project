using AttendanceSystem.Models;
using AttendanceSystem.Data.Repositories; // Assumes IClassSessionRepository exists
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AttendanceSystem.Services
{
    public class ClassSessionService : IClassSessionService
    {
        private readonly IClassSessionRepository _sessionRepository;

        public ClassSessionService(IClassSessionRepository sessionRepository)
        {
            _sessionRepository = sessionRepository;
        }

        public async Task<List<ClassSession>> GetAllSessionsAsync()
        {
            var sessions = await _sessionRepository.GetAllAsync();
            return new List<ClassSession>(sessions);
        }

        public async Task<ClassSession?> GetSessionByIdAsync(int sessionId)
        {
            return await _sessionRepository.GetByIdAsync(sessionId);
        }

        public async Task CreateSessionAsync(ClassSession session)
        {
            await _sessionRepository.AddAsync(session);
        }

        public async Task UpdateSessionAsync(ClassSession session)
        {
            await _sessionRepository.UpdateAsync(session);
        }

        public async Task DeleteSessionAsync(int sessionId)
        {
            var session = await _sessionRepository.GetByIdAsync(sessionId);
            if (session != null)
            {
                await _sessionRepository.DeleteAsync(session);
            }
        }
    }
}
