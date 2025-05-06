using AttendanceSystem.Models;
using AttendanceSystem.Data.Repositories; // Assumes IClassSessionRepository exists
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

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
            var sessions = await _sessionRepository.GetAllSessionsAsync();
            return new List<ClassSession>(sessions);
        }

        public async Task<List<ClassSession>> GetByCourseIDAsync(int courseID)
        {
            var sessions = await _sessionRepository.GetByCourseIDAsync(courseID);
            return new List<ClassSession>(sessions);
        }

        public async Task<ClassSession?> GetSessionByIdAsync(int sessionId)
        {
            return await _sessionRepository.GetSessionByIdAsync(sessionId);
        }

        public async Task CreateSessionAsync(ClassSession session)
        {
            await _sessionRepository.AddSessionAsync(session);
        }

        public async Task UpdateSessionAsync(ClassSession session)
        {
            await _sessionRepository.UpdateSessionAsync(session);
        }

        public async Task DeleteSessionAsync(int sessionId)
        {
            var session = await _sessionRepository.GetSessionByIdAsync(sessionId);
            if (session != null)
            {
                await _sessionRepository.DeleteSessionAsync(session);
            }
        }
        public async Task<List<ClassSession>> GetSessionBySessionDateTimeAsync(DateTime SessionDateTime)
        {
            var sessions = await _sessionRepository.GetSessionBySessionDateTimeAsync(SessionDateTime);
            return new List<ClassSession>(sessions);
        }

        public async Task<ClassSession?> GetCurrentSessionAsync()
        {
            var currentTime = DateTime.Now;
            var allSessions = await _sessionRepository.GetAllSessionsAsync();

            return allSessions
                .Where(s => s.QuizStartTime <= currentTime && currentTime <= s.QuizEndTime)
                .OrderByDescending(s => s.QuizStartTime) 
                .FirstOrDefault();
        }
    }
}
