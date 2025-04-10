using AttendanceSystem.Models;
using AttendanceSystem.Data.Repositories; // Assumes IQuizResponseRepository exists
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AttendanceSystem.Services
{
    public class QuizResponseService : IQuizResponseService
    {
        private readonly IQuizResponseRepository _responseRepository;

        public QuizResponseService(IQuizResponseRepository responseRepository)
        {
            _responseRepository = responseRepository;
        }

        public async Task<List<QuizResponse>> GetAllResponsesAsync()
        {
            var responses = await _responseRepository.GetAllAsync();
            return new List<QuizResponse>(responses);
        }

        public async Task<QuizResponse?> GetResponseByIdAsync(int responseId)
        {
            return await _responseRepository.GetByIdAsync(responseId);
        }

        public async Task CreateResponseAsync(QuizResponse response)
        {
            await _responseRepository.AddAsync(response);
        }

        public async Task UpdateResponseAsync(QuizResponse response)
        {
            await _responseRepository.UpdateAsync(response);
        }

        public async Task DeleteResponseAsync(int responseId)
        {
            var response = await _responseRepository.GetByIdAsync(responseId);
            if (response != null)
            {
                await _responseRepository.DeleteAsync(response);
            }
        }
    }
}
