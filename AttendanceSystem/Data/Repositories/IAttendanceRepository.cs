using AttendanceSystem.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AttendanceSystem.Data.Repositories;

public interface IAttendanceRepository
{
    Task<IEnumerable<Attendance>> GetAllAttendancesAsync();
    Task<Attendance?> GetAttendanceByIdAsync(int id);
    Task<IEnumerable<Attendance>> GetPresentAttendancesAsync();
    Task AddAttendanceAsync(Attendance attendance);
    Task UpdateAttendanceAsync(Attendance attendance);
    Task DeleteAttendanceAsync(Attendance attendance);
}