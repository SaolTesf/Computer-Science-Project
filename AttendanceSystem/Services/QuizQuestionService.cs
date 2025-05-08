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
            var questions = await _questionRepository.GetAllQuestionsAsync();
            return new List<QuizQuestion>(questions);
        }

        public async Task<QuizQuestion?> GetQuestionByIdAsync(int questionId)
        {
            return await _questionRepository.GetQuestionByIdAsync(questionId);
        }

        public async Task CreateQuestionAsync(QuizQuestion question)
        {
            await _questionRepository.AddQuestionAsync(question);
        }

        public async Task UpdateQuestionAsync(QuizQuestion question)
        {
            await _questionRepository.UpdateQuestionAsync(question);
        }

        public async Task DeleteQuestionAsync(int questionId)
        {
            var question = await _questionRepository.GetQuestionByIdAsync(questionId);
            if (question != null)
            {
                await _questionRepository.DeleteQuestionAsync(question);
            }
        }

        public async Task<List<QuizQuestion>> GetQuestionsByBankIdAsync(int bankId)
        {
            var questions = await _questionRepository.GetQuestionsByBankIdAsync(bankId);
            return questions?.ToList() ?? new List<QuizQuestion>();
        }

        public async Task<int?> GetQuestionIdByTextAsync(string questionText)
        {
            return await _questionRepository.GetQuestionIdByTextAsync(questionText);
        }
    }

}
