using AttendanceSystem.Models;
using AttendanceSystem.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AttendanceSystem.Services
{

    public class AttendanceService : IAttendanceService
    {
        private readonly IAttendanceRepository _attendanceRepository;

        public AttendanceService(IAttendanceRepository attendanceRepository)
        {
            _attendanceRepository = attendanceRepository;
        }

        public async Task<List<Attendance>> GetAllAttendancesAsync()
        {
            var result = await _attendanceRepository.GetAllAsync();
            return new List<Attendance>(result);
        }

        public async Task<Attendance?> GetAttendanceByIdAsync(int id)
        {
            return await _attendanceRepository.GetByIdAsync(id);
        }

        public async Task<List<Attendance>> GetPresentAttendancesAsync()
        {
            var result = await _attendanceRepository.GetPresentAttendancesAsync();
            return new List<Attendance>(result);
        }

        public async Task CreateAttendanceAsync(Attendance attendance)
        {

            await _attendanceRepository.AddAsync(attendance);
        }

        public async Task UpdateAttendanceAsync(Attendance attendance)
        {

            await _attendanceRepository.UpdateAsync(attendance);
        }

        public async Task DeleteAttendanceAsync(int id)
        {
            var attendance = await _attendanceRepository.GetByIdAsync(id);
            if (attendance != null)
            {
                await _attendanceRepository.DeleteAsync(attendance);
            }
        }
    }
}
