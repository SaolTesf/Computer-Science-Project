/*
Saol Tesfaghebriel
Professor Service class that implements the IProfessorService interface for managing professor data in the attendance system.
*/

using AttendanceSystem.Models;

namespace AttendanceSystem.Services;

public interface IProfessorService {
  Task<IEnumerable<Professor>> GetAllProfessorsAsync();
  Task<Professor?> GetProfessorByIdAsync(String id);
  Task<Professor?> GetProfessorByUsernameAsync(String username);
  Task<Professor?> GetProfessorByEmailAsync(String email);
  Task<bool> AddProfessorAsync(Professor professor);
  Task<bool> UpdateProfessorAsync(Professor professor);
  Task<bool> DeleteProfessorByIdAsync(String id);
  Task<bool> ProfessorExistsByIdAsync(String id);
  Task<bool> ProfessorExistsByUsernameAsync(String username);
  Task<bool> ProfessorExistsByEmailAsync(String email);
}