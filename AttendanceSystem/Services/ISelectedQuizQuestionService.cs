using AttendanceSystem.Models;

namespace AttendanceSystem.Services
{
    public interface ISelectedQuizQuestionService
    {
        Task AddSelectedQuestionsAsync(List<SelectedQuizQuestion> questions);
        Task<List<SelectedQuizQuestion>> GetAllSelectedQuestionsAsync();
        Task ClearSelectedQuestionsAsync();
    }
}
