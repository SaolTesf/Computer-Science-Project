using AttendanceSystem.Models;         // Import the Attendance model
using AttendanceSystem.Services;       // Import IAttendanceService
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AttendanceSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AttendanceController : ControllerBase
    {
        private readonly IAttendanceService _attendanceService; // service instance

        public AttendanceController(IAttendanceService attendanceService)
        {
            _attendanceService = attendanceService; // dependency injection
        }

        // GET: api/attendance
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Attendance>>> GetAllAttendances()
        {
            var attendances = await _attendanceService.GetAllAttendancesAsync();
            return Ok(attendances);
        }

        // GET: api/attendance/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Attendance>> GetAttendanceById(int id)
        {
            var attendance = await _attendanceService.GetAttendanceByIdAsync(id);
            if (attendance == null)
                return NotFound();
            return Ok(attendance);
        }

        // GET: api/attendance/present
        [HttpGet("present")]
        public async Task<ActionResult<IEnumerable<Attendance>>> GetPresentAttendances()
        {
            var attendances = await _attendanceService.GetPresentAttendancesAsync();
            return Ok(attendances);
        }

        // POST: api/attendance
        [HttpPost]
        public async Task<ActionResult> CreateAttendance([FromBody] Attendance attendance)
        {
            if (attendance == null)
                return BadRequest();

            await _attendanceService.CreateAttendanceAsync(attendance);
            return CreatedAtAction(nameof(GetAttendanceById), new { id = attendance.AttendanceID }, attendance);
        }

        // PUT: api/attendance/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAttendance(int id, [FromBody] Attendance attendance)
        {
            if (id != attendance.AttendanceID)
                return BadRequest("ID mismatch.");

            await _attendanceService.UpdateAttendanceAsync(attendance);
            return NoContent();
        }

        // DELETE: api/attendance/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAttendance(int id)
        {
            await _attendanceService.DeleteAttendanceAsync(id);
            return NoContent();
        }
    }
}
