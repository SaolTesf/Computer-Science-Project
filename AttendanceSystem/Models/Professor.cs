namespace AttendanceSystem.Models;

public class Professor {
  public string ID { get; set; } = null!;
  public string FirstName { get; set; } = null!;
  public string LastName { get; set; } = null!;
  public string Username { get; set; } = null!;
  public string Email { get; set; } = null!;
  public string PasswordHash { get; set; } = null!;

  //public virtual ICollection<Course> Courses { get; set; } = new List<Course>();

}