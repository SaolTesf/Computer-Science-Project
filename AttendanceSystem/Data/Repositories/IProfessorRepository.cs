using AttendanceSystem.Models;

namespace AttendanceSystem.Data.Repositories;

public interface IProfessorRepository {
  Task<IEnumerable<Professor>> GetAllProfessorsAsync();
  Task<Professor?> GetProfessorByIdAsync(String id);
  Task<Professor?> GetProfessorByUsernameAsync(String username);
  Task<Professor?> GetProfessorByEmailAsync(String email);
  Task AddProfessorAsync(Professor professor);
  Task UpdateProfessorAsync(Professor professor);
  Task DeleteProfessorAsync(Professor professor);
  Task<bool> ProfessorExistsAsync(String id);
  Task<bool> ProfessorExistsByUsernameAsync(String username);
  Task<bool> ProfessorExistsByEmailAsync(String email);
}