using AttendanceSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace AttendanceSystem.Data.Repositories;
public class AttendanceRepository : IAttendanceRepository
{
    private readonly AppDbContext _context; // EF Core DbContext

    public AttendanceRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Attendance>> GetAllAttendancesAsync()
    {
        return await _context.Attendances.ToListAsync(); // get all attendance recs
    }

    public async Task<Attendance?> GetAttendanceByIdAsync(int id)
    {
        return await _context.Attendances.FindAsync(id); // get a specific rec by ID
    }

    public async Task<IEnumerable<Attendance>> GetPresentAttendancesAsync()
    {
        return await _context.Attendances
            .Where(a => a.AttendanceType == AttendanceType.Present) // get records with AttendanceType Present
            .ToListAsync();
    }

    public async Task AddAttendanceAsync(Attendance attendance)
    {
        await _context.Attendances.AddAsync(attendance); // add a new rec to the db
        await _context.SaveChangesAsync();               // save changes
    }

    public async Task UpdateAttendanceAsync(Attendance attendance)
    {
        _context.Attendances.Update(attendance);         // update an existing rec
        await _context.SaveChangesAsync();               // save changes
    }

    public async Task DeleteAttendanceAsync(Attendance attendance)
    {
        _context.Attendances.Remove(attendance);         // delete rec
        await _context.SaveChangesAsync();               // save changes
    }

    public async Task<bool> ExistsAsync(string ipAddress, DateTime date)
    {
        // check if any record matches both IP and the date portion
        bool exists = await _context.Attendances
            .AnyAsync(a => a.IPAddress == ipAddress && a.SubmissionTime.Date == date.Date );  
        if (exists)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public async Task<bool> DateExistsAsync(DateTime date)
    {
        return await _context.Attendances
            .AnyAsync(a => a.SubmissionTime.Date == date.Date);
    }

}
