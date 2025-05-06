using AttendanceSystem.Data;
using AttendanceSystem.Models;
using AttendanceSystem.Services;
using Microsoft.EntityFrameworkCore;

public class AttendanceBackgroundService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<AttendanceBackgroundService> _logger;
    public bool IsAttendanceProcessed { get; set; } = false;


    public AttendanceBackgroundService(IServiceProvider serviceProvider,
                                       ILogger<AttendanceBackgroundService> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                //Wait for 1 minute before checking again
                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);

                using var scope = _serviceProvider.CreateScope();
                var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                var attendanceService = scope.ServiceProvider.GetRequiredService<IAttendanceService>();

                var now = DateTime.Now;

                //Find sessions where QuizEndTime was in the last minute
                var oneMinuteAgo = now.AddMinutes(-1);
                var sessionsWithQuizJustEnded = await db.ClassSessions
                    .Where(s => s.QuizEndTime > oneMinuteAgo && s.QuizEndTime <= now)
                    .ToListAsync(stoppingToken);

                foreach (var session in sessionsWithQuizJustEnded)
                {
                    //Get all enrolled students
                    var enrolledStudents = await db.CourseEnrollments
                        .Where(e => e.CourseID == session.CourseID)
                        .Select(e => e.UTDID)
                        .ToListAsync(stoppingToken);

                    //Get ALL attendance records (both submitted & system-generated)
                    var studentsWithAnyAttendance = await db.Attendances
                        .Where(a => a.SessionID == session.SessionID)
                        .Select(a => a.UTDID)
                        .Distinct()
                        .ToListAsync(stoppingToken);

                    //Only students with NO attendance record at all
                    var absentStudents = enrolledStudents.Except(studentsWithAnyAttendance).ToList();

                    foreach (var utdid in absentStudents)
                    {
                        var attendanceRecord = new Attendance
                        {
                            SessionID = session.SessionID,
                            UTDID = utdid,
                            SubmissionTime = session.QuizEndTime,
                            IPAddress = "N/A",
                            AttendanceType = AttendanceType.Unexcused
                        };

                        await attendanceService.CreateAttendanceAsync(attendanceRecord);
                        _logger.LogInformation($"Marked UTDID {utdid} as absent for Session {session.SessionID}");
                    }

                    await db.SaveChangesAsync(stoppingToken);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing attendance after quiz end.");
            }
        }
    }
}
