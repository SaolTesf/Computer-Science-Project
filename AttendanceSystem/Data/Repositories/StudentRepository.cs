using AttendanceSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace AttendanceSystem.Data.Repositories;

public class StudentRepository(AppDbContext context) : IStudentRepository {
  private readonly AppDbContext _context = context;

  public async Task<IEnumerable<Student>> GetAllStudentsAsync() {
    return await _context.Students.ToListAsync();
  }

  public async Task<Student?> GetStudentByUTDIdAsync(String id) {
    return await _context.Students.FindAsync(id);
  }

  public async Task<Student?> GetStudentByUsernameAsync(String username) {
    return await _context.Students.FirstOrDefaultAsync(p => p.Username == username);
  }
  public async Task AddStudentAsync(Student student) {
    await _context.Students.AddAsync(student);
    await _context.SaveChangesAsync();
  }

  public async Task UpdateStudentAsync(Student student) {
    _context.Students.Update(student);
    await _context.SaveChangesAsync();
  }

  public async Task DeleteStudentByUTDIdAsync(String id) {
    var student = await GetStudentByUTDIdAsync(id);
    if(student != null){
      _context.Students.Remove(student);
      await _context.SaveChangesAsync();
    }
  }
}