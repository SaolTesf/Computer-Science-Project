using AttendanceSystem.Components;
using AttendanceSystem.Data;
using AttendanceSystem.Data.Repositories;
using AttendanceSystem.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySQL(
        builder.Configuration.GetConnectionString("DefaultConnection")));

// Add professors repository
builder.Services.AddScoped<IProfessorRepository, ProfessorRepository>();

// Add professors service
builder.Services.AddScoped<IProfessorService, ProfessorService>();

<<<<<<< HEAD
builder.Services.AddScoped<IAttendanceRepository, AttendanceRepository>();

builder.Services.AddScoped<IAttendanceService, AttendanceService>();


=======
>>>>>>> dc81ff7998debd8cf4ac143e9ec715b1ae57409e
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
