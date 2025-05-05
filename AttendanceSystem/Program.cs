/*
    This is the main entry point for the ASP.NET Core application. It sets up the services, middleware, and routing for the application.
    It also configures the authentication and authorization for the application using JWT tokens.
    The application uses Entity Framework Core to connect to a MySQL database and includes a repository and service for managing professors.
*/

using System.Text;
using AttendanceSystem.Components;
using AttendanceSystem.Data;
using AttendanceSystem.Data.Repositories;
using AttendanceSystem.Repositories;
using AttendanceSystem.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;


var builder = WebApplication.CreateBuilder(args);

#pragma warning disable CS8604 // Possible null reference argument.
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySQL(
        builder.Configuration.GetConnectionString("DefaultConnection")));
#pragma warning restore CS8604 // Possible null reference argument.

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(
            builder.Configuration["Jwt:Key"] ?? throw new InvalidOperationException("JWT key not configured"))),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.AddScoped<IAuthService, AuthService>();

// Add professors repository
builder.Services.AddScoped<IProfessorRepository, ProfessorRepository>();

// Add professors service
builder.Services.AddScoped<IProfessorService, ProfessorService>();

builder.Services.AddScoped<IAttendanceRepository, AttendanceRepository>();

builder.Services.AddScoped<IAttendanceService, AttendanceService>();

builder.Services.AddScoped<ICourseRepository, CourseRepository>();

builder.Services.AddScoped<IClassSessionRepository, ClassSessionRepository>();

builder.Services.AddScoped<IQuizQuestionBankRepository, QuizQuestionBankRepository>();

builder.Services.AddScoped<IQuizQuestionRepository, QuizQuestionRepository>();

builder.Services.AddScoped<IQuizResponseRepository, QuizResponseRepository>();

builder.Services.AddScoped<ICourseService, CourseService>();

builder.Services.AddScoped<IClassSessionService, ClassSessionService>();

builder.Services.AddScoped<IQuizQuestionBankService, QuizQuestionBankService>();

builder.Services.AddScoped<IQuizQuestionService, QuizQuestionService>();

builder.Services.AddScoped<IQuizResponseService, QuizResponseService>();

builder.Services.AddScoped<IStudentRepository, StudentRepository>();

builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<ISelectedQuizQuestionRepository, SelectedQuizQuestionRepository>();
builder.Services.AddScoped<ISelectedQuizQuestionService, SelectedQuizQuestionService>();

// Register course enrollment repository & service
builder.Services.AddScoped<ICourseEnrollmentRepository, CourseEnrollmentRepository>();
builder.Services.AddScoped<ICourseEnrollmentService, CourseEnrollmentService>();

builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<GetIPService>();

builder.Services.AddHttpClient();

// Add professors controller
builder.Services.AddControllers();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
}

app.UseAuthentication();
app.UseAuthorization();



app.UseAntiforgery();

app.MapControllers();

app.UseStaticFiles();
app.MapRazorComponents<AttendanceSystem.Components.App>()
    .AddInteractiveServerRenderMode();

app.Run();
