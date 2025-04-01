using AttendanceSystem.Models;

using AttendanceSystem.Data.Repositories;

namespace AttendanceSystem.Services
{
    public interface IAttendanceService
    {
        Task<List<Attendance>> GetAllAttendancesAsync(); // all atendances
        Task<Attendance?> GetAttendanceByIdAsync(int id); // get by id
        Task<List<Attendance>> GetPresentAttendancesAsync(); // presnte attendance
        Task CreateAttendanceAsync(Attendance attendance); // create attendacen
        Task UpdateAttendanceAsync(Attendance attendance); // update attendance
        Task DeleteAttendanceAsync(int id); //deete 



    }
}
