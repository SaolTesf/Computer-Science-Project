namespace AttendanceSystem.Models.DTOs
{
    public class StudentDTO
    {
        // DTO for Student model to avoid circular reference
        public string UTDID { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Username { get; set; } = null!;
    }
}
