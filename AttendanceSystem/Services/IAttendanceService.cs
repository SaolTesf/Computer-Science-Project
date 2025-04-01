using AttendanceSystem.Models;
<<<<<<< HEAD
using AttendanceSystem.Repositories;

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
=======
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AttendanceSystem.Services
{
    // This interface defines the contract for business operations on Attendance records.
    public interface IAttendanceService
    {
        Task<List<Attendance>> GetAllAttendancesAsync();
        Task<Attendance?> GetAttendanceByIdAsync(int id);
        Task<List<Attendance>> GetPresentAttendancesAsync();
        Task CreateAttendanceAsync(Attendance attendance);
        Task UpdateAttendanceAsync(Attendance attendance);
        Task DeleteAttendanceAsync(int id);
>>>>>>> dc81ff7998debd8cf4ac143e9ec715b1ae57409e
    }
}
