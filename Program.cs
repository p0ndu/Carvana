using Carvana.Services;
using Carvana.Data;
using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;


var builder = WebApplication.CreateBuilder(args);

// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy =>
        {
            policy.WithOrigins("http://localhost:3000")
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        });
});

// following db connection code is not my own, found online through mix of documentation and chatGPT help
Env.Load("connection.env"); // load data for connection string
var connectionString = $"Host={Environment.GetEnvironmentVariable("DB_HOST")};" + // build connection string
                       $"Port={Environment.GetEnvironmentVariable("DB_PORT")};" +
                       $"Database={Environment.GetEnvironmentVariable("DB_NAME")};" +
                       $"Username={Environment.GetEnvironmentVariable("DB_USERNAME")};" +
                       $"Password={Environment.GetEnvironmentVariable("DB_PASSWORD")}";
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(connectionString)); // register context with Postgress provider

builder.Services.AddOpenApi();
// TreeService as singleton since autocomplete is almost always needed, rest as scoped services
builder.Services.AddSingleton<TreeService>();
builder.Services.AddScoped<CarService>();
builder.Services.AddScoped<CustomerService>();
builder.Services.AddControllers()
        .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });
var app = builder.Build();
// Use CORS
app.UseCors("AllowFrontend");


StartupService.Run(app.Services);

app.UseHttpsRedirection();
app.MapControllers();

app.Run();

