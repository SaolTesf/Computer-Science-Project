using AttendanceSystem.Models;
using AttendanceSystem.Repositories;


namespace AttendanceSystem.Services
{
    public class AttendanceService : IAttendanceService
    {
        private readonly IAttendanceRepository _attendanceRepository; // rep instance to get data

        public AttendanceService(IAttendanceRepository attendanceRepository)
        {
            _attendanceRepository = attendanceRepository; // rep dependency
        }

        public async Task<List<Attendance>> GetAllAttendancesAsync()
        {
            var result = await _attendanceRepository.GetAllAsync(); // get attendance recs fro rep
            return new List<Attendance>(result);                    // convert to list
        }

        public async Task<Attendance?> GetAttendanceByIdAsync(int id)
        {
            return await _attendanceRepository.GetByIdAsync(id);
        }

        public async Task<List<Attendance>> GetPresentAttendancesAsync()
        {
            var result = await _attendanceRepository.GetPresentAttendancesAsync(); // get presnt recs
            return new List<Attendance>(result);
        }

        public async Task CreateAttendanceAsync(Attendance attendance)
        {
            await _attendanceRepository.AddAsync(attendance);       // add a new rec via repository
        }

        public async Task UpdateAttendanceAsync(Attendance attendance)
        {
            await _attendanceRepository.UpdateAsync(attendance);    // update an existing rec
        }

        public async Task DeleteAttendanceAsync(int id)
        {
            var attendance = await _attendanceRepository.GetByIdAsync(id); // reterive rec to be deletd by ID
            if (attendance != null)
            {
                await _attendanceRepository.DeleteAsync(attendance);       // if it exists then we delete
            }
        }
    }
}
