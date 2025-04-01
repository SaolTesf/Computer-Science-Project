using AttendanceSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace AttendanceSystem.Data.Repositories;

public class ProfessorRepository(AppDbContext context) : IProfessorRepository {
  private readonly AppDbContext _context = context;

  public async Task<IEnumerable<Professor>> GetAllProfessorsAsync() {
    return await _context.Professors.ToListAsync();
  }

  public async Task<Professor?> GetProfessorByIdAsync(String id) {
    return await _context.Professors.FindAsync(id);
  }

  public async Task<Professor?> GetProfessorByUsernameAsync(String username) {
    return await _context.Professors.FirstOrDefaultAsync(p => p.Username == username);
  }

  public async Task<Professor?> GetProfessorByEmailAsync(String email) {
    return await _context.Professors.FirstOrDefaultAsync(p => p.Email == email);
  }

  public async Task AddProfessorAsync(Professor professor) {
    await _context.Professors.AddAsync(professor);
    await _context.SaveChangesAsync();
  }

  public async Task UpdateProfessorAsync(Professor professor) {
    _context.Professors.Update(professor);
    await _context.SaveChangesAsync();
  }

  public async Task DeleteProfessorByIdAsync(String id) {
    var professor = await GetProfessorByIdAsync(id);
    if(professor != null){
      _context.Professors.Remove(professor);
      await _context.SaveChangesAsync();
    }
  }

  public async Task<bool> ProfessorExistsByIdAsync(String id) {
    return await GetProfessorByIdAsync(id) != null;
  }

  public async Task<bool> ProfessorExistsByUsernameAsync(String username) {
    return await GetProfessorByUsernameAsync(username) != null;
  }

  public async Task<bool> ProfessorExistsByEmailAsync(String email) {
    return await GetProfessorByEmailAsync(email) != null;
  }
}