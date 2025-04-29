using AttendanceSystem.Models;
using AttendanceSystem.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AttendanceSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuizQuestionBankController : ControllerBase
    {
        private readonly IQuizQuestionBankService _quizQuestionBankService;

        public QuizQuestionBankController(IQuizQuestionBankService quizQuestionBankService)
        {
            _quizQuestionBankService = quizQuestionBankService;
        }

        // GET: api/quizquestionbank
        [HttpGet]
        public async Task<ActionResult<IEnumerable<QuizQuestionBank>>> GetAllBanks()
        {
            var banks = await _quizQuestionBankService.GetAllBanksAsync();
            return Ok(banks);
        }

        // GET: api/quizquestionbank/{bankId}
        [HttpGet("{bankId}")]
        public async Task<ActionResult<QuizQuestionBank>> GetBankById(int bankId)
        {
            var bank = await _quizQuestionBankService.GetBankByIdAsync(bankId);
            if (bank == null)
                return NotFound();
            return Ok(bank);
        }

        // POST: api/quizquestionbank
        [HttpPost]
        public async Task<ActionResult> CreateBank([FromBody] QuizQuestionBank bank)
        {
            if (bank == null)
                return BadRequest("Invalid quiz bank data.");

            //Validate that CourseID is provided and is valid
            if (bank.CourseID <= 0)
                return BadRequest("Invalid Course ID.");

            await _quizQuestionBankService.CreateBankAsync(bank);

            // Return NoContent to indicate successful creation with no content to return
            return NoContent();
        }

        // PUT: api/quizquestionbank/{bankId}
        [HttpPut("{bankId}")]
        public async Task<ActionResult> UpdateBank(int bankId, [FromBody] QuizQuestionBank bank)
        {
            if (bankId != bank.QuestionBankID)
                return BadRequest("ID mismatch.");
            await _quizQuestionBankService.UpdateBankAsync(bank);
            return NoContent();
        }

        // DELETE: api/quizquestionbank/{bankId}
        [HttpDelete("{bankId}")]
        public async Task<ActionResult> DeleteBank(int bankId)
        {
            await _quizQuestionBankService.DeleteBankAsync(bankId);
            return NoContent();
        }

        // GET: api/quizquestionbank/banknames
        [HttpGet("banknames")]
        public async Task<ActionResult<IEnumerable<string>>> GetAllBankNames()
        {
            var bankNames = await _quizQuestionBankService.GetAllBankNamesAsync();
            return Ok(bankNames);

        }

        [HttpGet("GetBankIdByName")]
        public async Task<IActionResult> GetBankIdByName([FromQuery] string bankName)
        {
            if (string.IsNullOrWhiteSpace(bankName))
            {
                return BadRequest("BankName cannot be null or empty.");
            }

            var bankId = await _quizQuestionBankService.GetQuestionBankIdByNameAsync(bankName);

            if (bankId == null)
            {
                return NotFound($"No Question Bank found with the name '{bankName}'.");
            }

            return Ok(bankId);
        }
        // GET: api/quizquestionbank/course/{courseID}
        [HttpGet("course/{courseID}")]
        public async Task<IActionResult> GetQuizBanksByCourseId(int courseID)
        {
            // Retrieve the banks that are associated with the specific course ID
            var banks = await _quizQuestionBankService.GetBanksByCourseIdAsync(courseID);

            if (banks == null)
            {
                return NotFound($"No quiz banks found for course with ID '{courseID}'.");
            }

            return Ok(banks);
        }
    }
}
