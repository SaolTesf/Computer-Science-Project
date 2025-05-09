using AttendanceSystem.Models;
using AttendanceSystem.Data.Repositories; // Assumes IQuizQuestionBankRepository exists
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
// Dinagaran Senthilkumar
// This is the QuizQuestionBankService.cs file that implements the IQuizQuestionBankService interface.
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
            var banks = await _bankRepository.GetAllBanksAsync();
            return new List<QuizQuestionBank>(banks);
        }

        public async Task<QuizQuestionBank?> GetBankByIdAsync(int bankId)
        {
            return await _bankRepository.GetBankByIdAsync(bankId);
        }

        public async Task CreateBankAsync(QuizQuestionBank bank)
        {
            await _bankRepository.AddBankAsync(bank);
        }

        public async Task UpdateBankAsync(QuizQuestionBank bank)
        {
            await _bankRepository.UpdateBankAsync(bank);
        }

        public async Task DeleteBankAsync(int bankId)
        {
            var bank = await _bankRepository.GetBankByIdAsync(bankId);
            if (bank != null)
            {
                await _bankRepository.DeleteBankAsync(bank);
            }
        }

        public async Task<IEnumerable<string>> GetAllBankNamesAsync()
        {
            return await _bankRepository.GetAllBankNamesAsync();
        }

        public async Task<int?> GetQuestionBankIdByNameAsync(string bankName)
        {
            return await _bankRepository.GetQuestionBankIdByNameAsync(bankName);
        }

        public async Task<List<QuizQuestionBank>> GetBanksByCourseIdAsync(int courseId)
        {
            return await _bankRepository.GetBanksByCourseIdAsync(courseId);
        }
    }
}
