using AttendanceSystem.Models;

public class SelectedQuizQuestion
{
    public int Id { get; set; }
    public string QuestionText { get; set; } = string.Empty;
    public int QuestionBankID { get; set; }  
    public DateTime SelectedAt { get; set; } = DateTime.UtcNow;
    public QuizQuestionBank QuestionBank { get; set; } = null!;
}
