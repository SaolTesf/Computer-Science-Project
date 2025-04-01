using AttendanceSystem.Data;
using AttendanceSystem.Models;
<<<<<<< HEAD

using AttendanceSystem.Repositories;

namespace AttendanceSystem.Repositories
{
    public class AttendanceRepository : IAttendanceRepository
    {
        private readonly AppDbContext _context; // EF Core DbContext
=======
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AttendanceSystem.Repositories
{
    // This class implements the data access logic using EF Core.
    public class AttendanceRepository : IAttendanceRepository
    {
        private readonly AppDbContext _context;
>>>>>>> dc81ff7998debd8cf4ac143e9ec715b1ae57409e

        public AttendanceRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Attendance>> GetAllAsync()
        {
<<<<<<< HEAD
            return await _context.Attendances.ToListAsync(); // get all attendance recs
=======
            return await _context.Attendances.ToListAsync();
>>>>>>> dc81ff7998debd8cf4ac143e9ec715b1ae57409e
        }

        public async Task<Attendance?> GetByIdAsync(int id)
        {
<<<<<<< HEAD
            return await _context.Attendances.FindAsync(id); // get a specific rec by ID
=======
            return await _context.Attendances.FindAsync(id);
>>>>>>> dc81ff7998debd8cf4ac143e9ec715b1ae57409e
        }

        public async Task<IEnumerable<Attendance>> GetPresentAttendancesAsync()
        {
            return await _context.Attendances
<<<<<<< HEAD
                .Where(a => a.AttendanceType == AttendanceType.Present) // get whose present
=======
                .Where(a => a.AttendanceType == AttendanceType.Present)
>>>>>>> dc81ff7998debd8cf4ac143e9ec715b1ae57409e
                .ToListAsync();
        }

        public async Task AddAsync(Attendance attendance)
        {
<<<<<<< HEAD
            await _context.Attendances.AddAsync(attendance); // add a new rec to the db
            await _context.SaveChangesAsync();               // save changes
=======
            await _context.Attendances.AddAsync(attendance);
            await _context.SaveChangesAsync();
>>>>>>> dc81ff7998debd8cf4ac143e9ec715b1ae57409e
        }

        public async Task UpdateAsync(Attendance attendance)
        {
<<<<<<< HEAD
            _context.Attendances.Update(attendance);         // update existing rec
            await _context.SaveChangesAsync();               // save changes
=======
            _context.Attendances.Update(attendance);
            await _context.SaveChangesAsync();
>>>>>>> dc81ff7998debd8cf4ac143e9ec715b1ae57409e
        }

        public async Task DeleteAsync(Attendance attendance)
        {
<<<<<<< HEAD
            _context.Attendances.Remove(attendance);         // delete rec
            await _context.SaveChangesAsync();               // save changes
=======
            _context.Attendances.Remove(attendance);
            await _context.SaveChangesAsync();
>>>>>>> dc81ff7998debd8cf4ac143e9ec715b1ae57409e
        }
    }
}
