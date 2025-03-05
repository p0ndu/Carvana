using Carvana.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSingleton<TreeService>();
builder.Services.AddControllers();
var app = builder.Build();


app.UseHttpsRedirection();
app.MapControllers();
app.Run();

