using AttendanceSystem.Models;
using AttendanceSystem.Repositories;
<<<<<<< HEAD


namespace AttendanceSystem.Services
{
    public class AttendanceService : IAttendanceService
    {
        private readonly IAttendanceRepository _attendanceRepository; // rep instance to get data

        public AttendanceService(IAttendanceRepository attendanceRepository)
        {
            _attendanceRepository = attendanceRepository; // rep dependency
=======
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
>>>>>>> dc81ff7998debd8cf4ac143e9ec715b1ae57409e
        }

        public async Task<List<Attendance>> GetAllAttendancesAsync()
        {
<<<<<<< HEAD
            var result = await _attendanceRepository.GetAllAsync(); // get attendance recs fro rep
            return new List<Attendance>(result);                    // convert to list
=======
            var result = await _attendanceRepository.GetAllAsync();
            return new List<Attendance>(result);
>>>>>>> dc81ff7998debd8cf4ac143e9ec715b1ae57409e
        }

        public async Task<Attendance?> GetAttendanceByIdAsync(int id)
        {
            return await _attendanceRepository.GetByIdAsync(id);
        }

        public async Task<List<Attendance>> GetPresentAttendancesAsync()
        {
<<<<<<< HEAD
            var result = await _attendanceRepository.GetPresentAttendancesAsync(); // get presnt recs
=======
            var result = await _attendanceRepository.GetPresentAttendancesAsync();
>>>>>>> dc81ff7998debd8cf4ac143e9ec715b1ae57409e
            return new List<Attendance>(result);
        }

        public async Task CreateAttendanceAsync(Attendance attendance)
        {
<<<<<<< HEAD
            await _attendanceRepository.AddAsync(attendance);       // add a new rec via repository
=======

            await _attendanceRepository.AddAsync(attendance);
>>>>>>> dc81ff7998debd8cf4ac143e9ec715b1ae57409e
        }

        public async Task UpdateAttendanceAsync(Attendance attendance)
        {
<<<<<<< HEAD
            await _attendanceRepository.UpdateAsync(attendance);    // update an existing rec
=======

            await _attendanceRepository.UpdateAsync(attendance);
>>>>>>> dc81ff7998debd8cf4ac143e9ec715b1ae57409e
        }

        public async Task DeleteAttendanceAsync(int id)
        {
<<<<<<< HEAD
            var attendance = await _attendanceRepository.GetByIdAsync(id); // reterive rec to be deletd by ID
            if (attendance != null)
            {
                await _attendanceRepository.DeleteAsync(attendance);       // if it exists then we delete
=======
            var attendance = await _attendanceRepository.GetByIdAsync(id);
            if (attendance != null)
            {
                await _attendanceRepository.DeleteAsync(attendance);
>>>>>>> dc81ff7998debd8cf4ac143e9ec715b1ae57409e
            }
        }
    }
}
