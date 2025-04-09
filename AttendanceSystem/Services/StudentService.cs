using AttendanceSystem.Data.Repositories;
using AttendanceSystem.Models;

namespace AttendanceSystem.Services {
  public class StudentService(IStudentRepository studentRepository) : IStudentService {
    private readonly IStudentRepository _studentRepository = studentRepository;
    
      public async Task<IEnumerable<Student>> GetAllStudentsAsync() {
        return await _studentRepository.GetAllStudentsAsync();
      }

    public async Task<Student?> GetStudentByUTDIdAsync(String id) {
      return await _studentRepository.GetStudentByUTDIdAsync(id);
    }

    public async Task<Student?> GetStudentByUsernameAsync(String username) {
      return await _studentRepository.GetStudentByUsernameAsync(username);
    }

    public async Task<bool> AddStudentAsync(Student student) {
      try{
        await _studentRepository.AddStudentAsync(student);
        return true;
      }
      catch{
        return false;
      }
    }

    public async Task<bool> UpdateStudentAsync(Student student) {
      try{
        await _studentRepository.UpdateStudentAsync(student);
        return true;
      }
      catch{
        return false;
      }
    }

    public async Task<bool> DeleteStudentByUTDIdAsync(String id) {
      try{
        await _studentRepository.DeleteStudentByUTDIdAsync(id);
        return true;
      }
      catch{
        return false;
      }
    }

    public async Task<bool> StudentExistsByUTDIdAsync(String id) {
      return await GetStudentByUTDIdAsync(id) != null;
    }
  }
}
