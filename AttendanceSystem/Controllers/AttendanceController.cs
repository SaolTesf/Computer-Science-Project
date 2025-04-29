using AttendanceSystem.Models;         // Import the Attendance model
using AttendanceSystem.Services;       // Import IAttendanceService
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AttendanceSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AttendanceController(IAttendanceService attendanceService) : ControllerBase
    {
        private readonly IAttendanceService _attendanceService = attendanceService; // service instance

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

        /// GET /api/attendance/exists?ipAddress={numbers}/date={numbers}
        [HttpGet("exists")]
        public async Task<ActionResult<bool>> Exists(string ipAddress,DateTime date)
        {
            var exists = await _attendanceService.RecordExistsAsync(ipAddress, date.Date);

            if (exists)
            {
                return Ok(true);
            }
            else
            {
                return Ok(false);
            }
        }

        // GET: api/attendance/date-exists?date=2025-04-21
        [HttpGet("date-exists")]
        public async Task<ActionResult<bool>> CheckDateExists([FromQuery] DateTime date)
        {
            var exists = await _attendanceService.DateExistsAsync(date.Date);
            return Ok(exists);
        }

        // GET: api/attendance/course/{courseID}
        [HttpGet("course/{courseID}")]
        public async Task<ActionResult<IEnumerable<Attendance>>> GetByCourseID(int courseID)
        {
            var attendances = await _attendanceService.GetByCourseIDAsync(courseID);
            return Ok(attendances);
        }

        [HttpGet("GetByUtdId/{utdId}")]
        public async Task<IActionResult> GetAttendanceByUtdId(string utdId)
        {
            var attendanceRecords = await _attendanceService.GetAttendanceByUtdIdAsync(utdId);
            if (attendanceRecords == null || !attendanceRecords.Any())
            {
                return NotFound($"No attendance records found for UTD ID {utdId}.");
            }
            return Ok(attendanceRecords);
        }
    }
}
