using AttendanceSystem.Models;

namespace AttendanceSystem.Data.Repositories;

public interface IStudentRepository {
  Task<IEnumerable<Student>> GetAllStudentsAsync();
  Task<Student?> GetStudentByUTDIdAsync(String id);
  Task<Student?> GetStudentByUsernameAsync(String username);
  Task AddStudentAsync(Student student);
  Task UpdateStudentAsync(Student student);
  Task DeleteStudentByUTDIdAsync(String id);
  Task<bool> StudentExistsByUTDIdAsync(String id);
}