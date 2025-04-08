using AttendanceSystem.Models;

namespace AttendanceSystem.Services;

public interface IStudentService {
  Task<IEnumerable<Student>> GetAllStudentsAsync();
  Task<Student?> GetStudentByUTDIdAsync(String id);
  Task<bool> AddStudentAsync(Student student);
  Task<bool> UpdateStudentAsync(Student student);
  Task<bool> DeleteStudentByUTDIdAsync(String id);
  Task<bool> StudentExistsByUTDIdAsync(String id);
}