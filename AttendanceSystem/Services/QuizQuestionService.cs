using AttendanceSystem.Models;
using AttendanceSystem.Data.Repositories; // Assumes IQuizQuestionRepository exists
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AttendanceSystem.Services
{
    public class QuizQuestionService : IQuizQuestionService
    {
        private readonly IQuizQuestionRepository _questionRepository;

        public QuizQuestionService(IQuizQuestionRepository questionRepository)
        {
            _questionRepository = questionRepository;
        }

        public async Task<List<QuizQuestion>> GetAllQuestionsAsync()
        {
            var questions = await _questionRepository.GetAllAsync();
            return new List<QuizQuestion>(questions);
        }

        public async Task<QuizQuestion?> GetQuestionByIdAsync(int questionId)
        {
            return await _questionRepository.GetByIdAsync(questionId);
        }

        public async Task CreateQuestionAsync(QuizQuestion question)
        {
            await _questionRepository.AddAsync(question);
        }

        public async Task UpdateQuestionAsync(QuizQuestion question)
        {
            await _questionRepository.UpdateAsync(question);
        }

        public async Task DeleteQuestionAsync(int questionId)
        {
            var question = await _questionRepository.GetByIdAsync(questionId);
            if (question != null)
            {
                await _questionRepository.DeleteAsync(question);
            }
        }
    }
}
