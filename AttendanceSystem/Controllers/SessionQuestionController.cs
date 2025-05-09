/*
Saol Tesfaghebriel
This is the controller for managing session questions in the Attendance System API.
*/

using AttendanceSystem.Models;
using AttendanceSystem.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AttendanceSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SessionQuestionController : ControllerBase {
        private readonly ISessionQuestionService _service;
        public SessionQuestionController(ISessionQuestionService service) {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SessionQuestion>>> GetAll() {
            var items = await _service.GetAllAsync();
            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SessionQuestion>> GetById(int id) {
            var item = await _service.GetByIdAsync(id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        [HttpGet("session/{sessionId}")]
        public async Task<ActionResult<IEnumerable<SessionQuestion>>> GetBySession(int sessionId) {
            var items = await _service.GetBySessionIdAsync(sessionId);
            return Ok(items);
        }

        [HttpGet("question/{questionId}")]
        public async Task<ActionResult<IEnumerable<SessionQuestion>>> GetByQuestion(int questionId) {
            var items = await _service.GetByQuestionIdAsync(questionId);
            return Ok(items);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] SessionQuestion entity) {
            if (entity == null) return BadRequest();
            await _service.CreateAsync(entity);
            return CreatedAtAction(nameof(GetById), new { id = entity.SessionQuestionID }, entity);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] SessionQuestion entity) {
            if (entity == null || id != entity.SessionQuestionID) return BadRequest();
            await _service.UpdateAsync(entity);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id) {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}