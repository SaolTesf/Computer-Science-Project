using AttendanceSystem.Models;
using AttendanceSystem.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AttendanceSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AttendanceController : ControllerBase
    {
        private readonly IAttendanceService _attendanceService;

        public AttendanceController(IAttendanceService attendanceService)
        {
            _attendanceService = attendanceService;
        }

        // GET: attendance
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Attendance>>> GetAllAttendances()
        {
            var attendances = await _attendanceService.GetAllAttendancesAsync();
            return Ok(attendances);
        }

        // GET: id
        [HttpGet("{id}")]
        public async Task<ActionResult<Attendance>> GetAttendanceById(int id)
        {
            var attendance = await _attendanceService.GetAttendanceByIdAsync(id);
            if (attendance == null)
                return NotFound();
            return Ok(attendance);
        }

        // GET: present students
        [HttpGet("present")]
        public async Task<ActionResult<IEnumerable<Attendance>>> GetPresentAttendances()
        {
            var attendances = await _attendanceService.GetPresentAttendancesAsync();
            return Ok(attendances);
        }

        // POST: attendance
        [HttpPost]
        public async Task<ActionResult> CreateAttendance([FromBody] Attendance attendance)
        {
            if (attendance == null)
                return BadRequest();

            await _attendanceService.CreateAttendanceAsync(attendance);
            return CreatedAtAction(nameof(GetAttendanceById), new { id = attendance.AttendanceID }, attendance);
        }

        // PUT: id
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAttendance(int id, [FromBody] Attendance attendance)
        {
            if (id != attendance.AttendanceID)
                return BadRequest("ID mismatch.");

            await _attendanceService.UpdateAttendanceAsync(attendance);
            return NoContent();
        }

        // DELETE: id
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAttendance(int id)
        {
            await _attendanceService.DeleteAttendanceAsync(id);
            return NoContent();
        }
    }
}
