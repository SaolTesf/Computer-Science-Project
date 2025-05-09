using AttendanceSystem.Models;
using AttendanceSystem.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

// Dinagaran Senthilkumar
// QuizQuestionController.cs file that handles HTTP requests related to question records.  I added the links to help me test it on postman easeir
namespace AttendanceSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuizQuestionController : ControllerBase
    {
        private readonly IQuizQuestionService _quizQuestionService;

        public QuizQuestionController(IQuizQuestionService quizQuestionService)
        {
            _quizQuestionService = quizQuestionService;
        }

        // GET: api/quizquestion
        [HttpGet]
        public async Task<ActionResult<IEnumerable<QuizQuestion>>> GetAllQuestions()
        {
            var questions = await _quizQuestionService.GetAllQuestionsAsync();
            return Ok(questions);
        }

        // GET: api/quizquestion/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<QuizQuestion>> GetQuestionById(int id)
        {
            var question = await _quizQuestionService.GetQuestionByIdAsync(id);
            if (question == null)
                return NotFound();
            return Ok(question);
        }

        // POST: api/quizquestion
        [HttpPost]
        public async Task<ActionResult> CreateQuestion([FromBody] QuizQuestion question)
        {
            if (question == null)
                return BadRequest();
            await _quizQuestionService.CreateQuestionAsync(question);
            return CreatedAtAction(nameof(GetQuestionById), new { id = question.QuestionID }, question);
        }

        // PUT: api/quizquestion/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateQuestion(int id, [FromBody] QuizQuestion question)
        {
            if (id != question.QuestionID)
                return BadRequest("ID mismatch.");
            await _quizQuestionService.UpdateQuestionAsync(question);
            return NoContent();
        }

        // DELETE: api/quizquestion/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteQuestion(int id)
        {
            await _quizQuestionService.DeleteQuestionAsync(id);
            return NoContent();
        }

        [HttpGet("GetByBankId/{bankId}")]
        public async Task<IActionResult> GetQuestionsByBankId(int bankId)
        {
            var questions = await _quizQuestionService.GetQuestionsByBankIdAsync(bankId);
            if (questions == null || !questions.Any())
            {
                return NotFound($"No questions found for bank ID {bankId}.");
            }
            return Ok(questions);
        }

        // GET: api/quizquestion/GetQuestionIdByText?text=someText
        [HttpGet("GetQuestionIdByText")]
        public async Task<ActionResult<int?>> GetQuestionIdByText([FromQuery] string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return BadRequest("Question text is required.");

            var questionId = await _quizQuestionService.GetQuestionIdByTextAsync(text);

            if (questionId == null)
                return NotFound("Question not found.");

            return Ok(questionId);
        }
    }
}
