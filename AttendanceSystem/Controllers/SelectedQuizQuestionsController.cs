/*
This file is the controller for managing selected quiz questions in the Attendance System API.
*/

using AttendanceSystem.Models;
using AttendanceSystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace AttendanceSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SelectedQuizQuestionsController : ControllerBase
    {
        private readonly ISelectedQuizQuestionService _selectedQuizQuestionService;

        public SelectedQuizQuestionsController(ISelectedQuizQuestionService selectedQuizQuestionService)
        {
            _selectedQuizQuestionService = selectedQuizQuestionService;
        }

        // POST: api/SelectedQuizQuestions
        [HttpPost]
        public async Task<IActionResult> AddSelectedQuestions([FromBody] List<SelectedQuizQuestion> selectedQuestions)
        {
            if (selectedQuestions == null || selectedQuestions.Count == 0)
            {
                return BadRequest("No questions provided.");
            }

            try
            {
                await _selectedQuizQuestionService.AddSelectedQuestionsAsync(selectedQuestions);
                return Ok("Questions saved successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/SelectedQuizQuestions
        [HttpGet]
        public async Task<ActionResult<List<SelectedQuizQuestion>>> GetAllSelectedQuestions()
        {
            try
            {
                var questions = await _selectedQuizQuestionService.GetAllSelectedQuestionsAsync();
                return Ok(questions);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE: api/SelectedQuizQuestions
        [HttpDelete]
        public async Task<IActionResult> ClearSelectedQuestions()
        {
            try
            {
                await _selectedQuizQuestionService.ClearSelectedQuestionsAsync();
                return Ok("All selected questions cleared.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
