using AttendanceSystem.Data;
using AttendanceSystem.Data.Repositories;
using AttendanceSystem.Services;
using Microsoft.EntityFrameworkCore;
using AttendanceSystem.Components;

var builder = WebApplication.CreateBuilder(args);

#pragma warning disable CS8604
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySQL(
        builder.Configuration.GetConnectionString("DefaultConnection")));
#pragma warning restore CS8604

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


app.UseAntiforgery();

app.MapControllers();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
