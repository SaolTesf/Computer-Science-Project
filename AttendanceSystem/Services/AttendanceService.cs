using AttendanceSystem.Models;
using AttendanceSystem.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AttendanceSystem.Services
{
    public class AttendanceService : IAttendanceService
    {
        private readonly IAttendanceRepository _attendanceRepository; // repository instance

        public AttendanceService(IAttendanceRepository attendanceRepository)
        {
            _attendanceRepository = attendanceRepository; // dependency injection
        }

        public async Task<List<Attendance>> GetAllAttendancesAsync()
        {
            var result = await _attendanceRepository.GetAllAttendancesAsync(); // get recs from repository
            return new List<Attendance>(result);                    // convert to list and return
        }

        public async Task<Attendance?> GetAttendanceByIdAsync(int id)
        {
            return await _attendanceRepository.GetAttendanceByIdAsync(id);   // get rec by ID
        }

        public async Task<List<Attendance>> GetPresentAttendancesAsync()
        {
            var result = await _attendanceRepository.GetPresentAttendancesAsync(); // get recs where type is Present
            return new List<Attendance>(result);                    // convert to list and return
        }

        public async Task CreateAttendanceAsync(Attendance attendance)
        {
            await _attendanceRepository.AddAttendanceAsync(attendance);       // add a new rec via repository
        }

        public async Task UpdateAttendanceAsync(Attendance attendance)
        {
            await _attendanceRepository.UpdateAttendanceAsync(attendance);    // update an existing rec via repository
        }

        public async Task DeleteAttendanceAsync(int id)
        {
            var attendance = await _attendanceRepository.GetAttendanceByIdAsync(id); // get rec to delete
            if (attendance != null)
            {
                await _attendanceRepository.DeleteAttendanceAsync(attendance);       // delete rec if found
            }

        }

        public async Task<bool> RecordExistsAsync(string ipAddress, DateTime date)
        {
            var exists = await _attendanceRepository.ExistsAsync(ipAddress, date);

            if (exists)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}