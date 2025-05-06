using AttendanceSystem.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AttendanceSystem.Data.Repositories
{
    public interface ISessionQuestionRepository
    {
        Task<IEnumerable<SessionQuestion>> GetAllAsync();
        Task<SessionQuestion?> GetByIdAsync(int id);
        Task<IEnumerable<SessionQuestion>> GetBySessionIdAsync(int sessionId);
        Task<IEnumerable<SessionQuestion>> GetByQuestionIdAsync(int questionId);
        Task AddAsync(SessionQuestion entity);
        Task UpdateAsync(SessionQuestion entity);
        Task DeleteAsync(SessionQuestion entity);
    }
}