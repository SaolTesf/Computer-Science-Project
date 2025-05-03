using AttendanceSystem.Models;
using AttendanceSystem.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Diagnostics;
using AttendanceShared.DTOs;

namespace AttendanceSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClassSessionController : ControllerBase
    {
        private readonly IClassSessionService _classSessionService;

        public ClassSessionController(IClassSessionService classSessionService)
        {
            _classSessionService = classSessionService;
        }

        // GET: api/classsession
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClassSession>>> GetAllSessions()
        {
            var sessions = await _classSessionService.GetAllSessionsAsync();
            return Ok(sessions);
        }

        // GET: api/classsession/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ClassSession>> GetSessionById(int id)
        {
            var session = await _classSessionService.GetSessionByIdAsync(id);
            if (session == null)
                return NotFound();
            return Ok(session);
        }


        // GET: api/classsession/course/{courseID} (all sessions with course ID)
        [HttpGet("course/{courseID}")]
        public async Task<ActionResult<IEnumerable<ClassSession>>> GetByCourseIDAsync(int courseID)
        {
            var sessions = await _classSessionService.GetByCourseIDAsync(courseID);
            var dto = sessions.Select(e => new ClassSessionDTO
            {
                SessionID = e.SessionID,
                CourseID = e.CourseID,
                SessionDateTime = e.SessionDateTime,
                QuizStartTime = e.QuizStartTime,
                QuizEndTime = e.QuizEndTime,
                Password = e.Password,
                QuestionBankID = e.QuestionBankID
            }).ToList();
            return Ok(dto);
        }

        // POST: api/classsession
        [HttpPost]
        public async Task<ActionResult> CreateSession([FromBody] ClassSession session)
        {
            if (session == null)
            {
                return BadRequest();
            }
            await _classSessionService.CreateSessionAsync(session);
            return CreatedAtAction(nameof(GetSessionById), new { id = session.SessionID }, session);
        }

        // PUT: api/classsession/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateSession(int id, [FromBody] ClassSession session)
        {
            if (id != session.SessionID)
                return BadRequest("ID mismatch.");
            await _classSessionService.UpdateSessionAsync(session);
            return NoContent();
        }

        // DELETE: api/classsession/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteSession(int id)
        {
            await _classSessionService.DeleteSessionAsync(id);
            return NoContent();
        }

        // GET: api/classsession/{dateTime}
        [HttpGet("{sessionDateTime}")]
        public async Task<ActionResult<ClassSession>> GetSessionBySessionDateTime(DateTime SessionDateTime)
        {
            var session = await _classSessionService.GetSessionBySessionDateTimeAsync(SessionDateTime);
            if (session == null)
                return NotFound();
            return Ok(session);
        }
    }
}
