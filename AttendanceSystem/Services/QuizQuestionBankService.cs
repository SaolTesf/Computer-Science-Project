using AttendanceSystem.Models;
using AttendanceSystem.Data.Repositories; // Assumes IQuizQuestionBankRepository exists
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AttendanceSystem.Services
{
    public class QuizQuestionBankService : IQuizQuestionBankService
    {
        private readonly IQuizQuestionBankRepository _bankRepository;

        public QuizQuestionBankService(IQuizQuestionBankRepository bankRepository)
        {
            _bankRepository = bankRepository;
        }

        public async Task<List<QuizQuestionBank>> GetAllBanksAsync()
        {
            var banks = await _bankRepository.GetAllAsync();
            return new List<QuizQuestionBank>(banks);
        }

        public async Task<QuizQuestionBank?> GetBankByIdAsync(int bankId)
        {
            return await _bankRepository.GetByIdAsync(bankId);
        }

        public async Task CreateBankAsync(QuizQuestionBank bank)
        {
            await _bankRepository.AddAsync(bank);
        }

        public async Task UpdateBankAsync(QuizQuestionBank bank)
        {
            await _bankRepository.UpdateAsync(bank);
        }

        public async Task DeleteBankAsync(int bankId)
        {
            var bank = await _bankRepository.GetByIdAsync(bankId);
            if (bank != null)
            {
                await _bankRepository.DeleteAsync(bank);
            }
        }
    }
}
