using Carvana.Services;
using Carvana.Data;
using DotNetEnv;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// following db connection code is not my own, found online through mix of documentation and chatGPT help
Env.Load("connection.env"); // load data for connection string
var connectionString = $"Host={Environment.GetEnvironmentVariable("DB_HOST")};" + // build connection string
                       $"Port={Environment.GetEnvironmentVariable("DB_PORT")};" +
                       $"Database={Environment.GetEnvironmentVariable("DB_NAME")};" +
                       $"Username={Environment.GetEnvironmentVariable("DB_USERNAME")};" +
                       $"Password={Environment.GetEnvironmentVariable("DB_PASSWORD")}";
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(connectionString)); // register context with Postgress provider

builder.Services.AddOpenApi();
builder.Services.AddSingleton<TreeService>();
builder.Services.AddControllers();
var app = builder.Build();


StartupService.Run(app.Services);

app.UseHttpsRedirection();
app.MapControllers();

app.Run();

