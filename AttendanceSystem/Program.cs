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

builder.Services.AddScoped<IStudentRepository, StudentRepository>();

builder.Services.AddScoped<IStudentService, StudentService>();

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



app.UseAntiforgery();

app.MapControllers();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
