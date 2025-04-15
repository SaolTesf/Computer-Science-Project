/*
Saol Tesfaghebriel
Professor Service class that implements the IProfessorService interface for managing professor data in the attendance system.
*/

using AttendanceSystem.Data.Repositories;
using AttendanceSystem.Models;

namespace AttendanceSystem.Services {
  public class ProfessorService(IProfessorRepository professorRepository) : IProfessorService {
    private readonly IProfessorRepository _professorRepository = professorRepository;
    
      public async Task<IEnumerable<Professor>> GetAllProfessorsAsync() {
        return await _professorRepository.GetAllProfessorsAsync();
      }

    public async Task<Professor?> GetProfessorByIdAsync(String id) {
      return await _professorRepository.GetProfessorByIdAsync(id);
    }

    public async Task<Professor?> GetProfessorByUsernameAsync(String username) {
      return await _professorRepository.GetProfessorByUsernameAsync(username);
    }

    public async Task<Professor?> GetProfessorByEmailAsync(String email) {
      return await _professorRepository.GetProfessorByEmailAsync(email);
    }

    public async Task<bool> AddProfessorAsync(Professor professor) {
      try{
        await _professorRepository.AddProfessorAsync(professor);
        return true;
      }
      catch{
        return false;
      }
    }

    public async Task<bool> UpdateProfessorAsync(Professor professor) {
      try{
        await _professorRepository.UpdateProfessorAsync(professor);
        return true;
      }
      catch{
        return false;
      }
    }

    public async Task<bool> DeleteProfessorByIdAsync(String id) {
      try{
        await _professorRepository.DeleteProfessorByIdAsync(id);
        return true;
      }
      catch{
        return false;
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
}
