using AttendanceSystem.Models;
using AttendanceSystem.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

// Dinagaran Senthilkumar
// Quiz ResponseControler.cs file that handles HTTP requests related to quizResponse records.  I added the links to help me test it on postman easeir
namespace AttendanceSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuizResponseController : ControllerBase
    {
        private readonly IQuizResponseService _quizResponseService;

        public QuizResponseController(IQuizResponseService quizResponseService)
        {
            _quizResponseService = quizResponseService;
        }

        // GET: api/quizresponse
        [HttpGet]
        public async Task<ActionResult<IEnumerable<QuizResponse>>> GetAllResponses()
        {
            var responses = await _quizResponseService.GetAllResponsesAsync();
            return Ok(responses);
        }

        // GET: api/quizresponse/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<QuizResponse>> GetResponseById(int id)
        {
            var response = await _quizResponseService.GetResponseByIdAsync(id);
            if (response == null)
                return NotFound();
            return Ok(response);
        }

        // POST: api/quizresponse
        [HttpPost]
        public async Task<ActionResult> CreateResponse([FromBody] QuizResponse response)
        {
            if (response == null)
                return BadRequest();
            await _quizResponseService.CreateResponseAsync(response);
            return CreatedAtAction(nameof(GetResponseById), new { id = response.ResponseID }, response);
        }

        // PUT: api/quizresponse/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateResponse(int id, [FromBody] QuizResponse response)
        {
            if (id != response.ResponseID)
                return BadRequest("ID mismatch.");
            await _quizResponseService.UpdateResponseAsync(response);
            return NoContent();
        }

        // DELETE: api/quizresponse/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteResponse(int id)
        {
            await _quizResponseService.DeleteResponseAsync(id);
            return NoContent();
        }
    }
}
