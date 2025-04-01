using AttendanceSystem.Data;
using AttendanceSystem.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AttendanceSystem.Repositories
{

    public class AttendanceRepository : IAttendanceRepository
    {
        private readonly AppDbContext _context; // database context

        public AttendanceRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Attendance>> GetAllAsync()
        {
            return await _context.Attendances.ToListAsync();  // get all attendance recs
        }

        public async Task<Attendance?> GetByIdAsync(int id)
        {
            return await _context.Attendances.FindAsync(id); // get a specific rec by ID
        }

        public async Task<IEnumerable<Attendance>> GetPresentAttendancesAsync()
        {
            return await _context.Attendances
                .Where(a => a.AttendanceType == AttendanceType.Present) // get whose present
                .ToListAsync();
        }

        public async Task AddAsync(Attendance attendance)
        {
            await _context.Attendances.AddAsync(attendance); // add a new rec to the db
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Attendance attendance)
        {
            _context.Attendances.Update(attendance); // update existing rec
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Attendance attendance)
        {
            _context.Attendances.Remove(attendance); // delete recs
            await _context.SaveChangesAsync();
        }
    }
}