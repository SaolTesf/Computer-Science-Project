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

    Task<bool> ExistsAsync(string ipAddress, DateTime date);
    Task<bool> DateExistsAsync(DateTime date);

    Task<List<Attendance>> GetAttendanceByUtdIdAsync(string utdId);


}