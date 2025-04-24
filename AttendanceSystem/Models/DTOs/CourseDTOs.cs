// Sawyer Kamman
// DTOs to manage courses

namespace AttendanceSystem.Models.DTOs
{
public class CourseDTO
{
    public string CourseNumber { get; set; } = null!;
    public string CourseName { get; set; } = null!;
    public string Section { get; set; } = null!;
    public string ProfessorID { get; set; } = null!;
    public int? CourseID { get; set; }
}

}
