using AttendanceSystem.Models;
using AttendanceSystem.Data.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AttendanceSystem.Services
{
    public class SessionQuestionService : ISessionQuestionService {
        private readonly ISessionQuestionRepository _repository;
        public SessionQuestionService(ISessionQuestionRepository repository) => _repository = repository;

        public async Task<List<SessionQuestion>> GetAllAsync() {
            var items = await _repository.GetAllAsync();
            return new List<SessionQuestion>(items);
        }

        public async Task<SessionQuestion?> GetByIdAsync(int id)
            => await _repository.GetByIdAsync(id);

        public async Task<List<SessionQuestion>> GetBySessionIdAsync(int sessionId) {
            var items = await _repository.GetBySessionIdAsync(sessionId);
            return new List<SessionQuestion>(items);
        }

        public async Task<List<SessionQuestion>> GetByQuestionIdAsync(int questionId) {
            var items = await _repository.GetByQuestionIdAsync(questionId);
            return new List<SessionQuestion>(items);
        }

        public async Task CreateAsync(SessionQuestion entity)
            => await _repository.AddAsync(entity);

        public async Task UpdateAsync(SessionQuestion entity)
            => await _repository.UpdateAsync(entity);

        public async Task DeleteAsync(int id) {
            var entity = await _repository.GetByIdAsync(id);
            if (entity != null)
                await _repository.DeleteAsync(entity);
        }
    }
}