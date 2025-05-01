using AttendanceSystem.Models;
using AttendanceSystem.Repositories;

namespace AttendanceSystem.Services
{
    public class SelectedQuizQuestionService : ISelectedQuizQuestionService
    {
        private readonly ISelectedQuizQuestionRepository _repository;

        public SelectedQuizQuestionService(ISelectedQuizQuestionRepository repository)
        {
            _repository = repository;
        }

        public async Task AddSelectedQuestionsAsync(List<SelectedQuizQuestion> questions)
        {
            await _repository.AddSelectedQuestionsAsync(questions);
        }

        public async Task<List<SelectedQuizQuestion>> GetAllSelectedQuestionsAsync()
        {
            return await _repository.GetAllSelectedQuestionsAsync();
        }

        public async Task ClearSelectedQuestionsAsync()
        {
            await _repository.ClearSelectedQuestionsAsync();
        }
    }
}
