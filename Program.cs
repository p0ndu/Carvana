using Carvana.Services;
using Carvana.Data;
using CarRentalAPI.Helpers;
using DotNetEnv;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy =>
        {
            policy.WithOrigins("http://localhost:3001")
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        });
});

// Database connection setup (kept as-is)
Env.Load("connection.env"); // Load data for connection string
var connectionString = $"Host={Environment.GetEnvironmentVariable("DB_HOST")};" +
                       $"Port={Environment.GetEnvironmentVariable("DB_PORT")};" +
                       $"Database={Environment.GetEnvironmentVariable("DB_NAME")};" +
                       $"Username={Environment.GetEnvironmentVariable("DB_USERNAME")};" +
                       $"Password={Environment.GetEnvironmentVariable("DB_PASSWORD")}";
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(connectionString));

// Registering services
builder.Services.AddOpenApi();
builder.Services.AddSingleton<TreeService>();
builder.Services.AddScoped<CarService>();
builder.Services.AddScoped<CustomerService>();
builder.Services.AddSingleton<EncryptionHelper>(); // 🔥 Added EncryptionHelper
builder.Services.AddControllers();

var app = builder.Build();

// Use CORS
app.UseCors("AllowFrontend");

StartupService.Run(app.Services);

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
