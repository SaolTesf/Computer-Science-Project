using AttendanceSystem.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AttendanceSystem.Services
{
    public interface ISessionQuestionService
    {
        Task<List<SessionQuestion>> GetAllAsync();
        Task<SessionQuestion?> GetByIdAsync(int id);
        Task<List<SessionQuestion>> GetBySessionIdAsync(int sessionId);
        Task<List<SessionQuestion>> GetByQuestionIdAsync(int questionId);
        Task CreateAsync(SessionQuestion entity);
        Task UpdateAsync(SessionQuestion entity);
        Task DeleteAsync(int id);
    }
}