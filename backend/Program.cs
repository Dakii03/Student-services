using backend.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddControllers();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        options.JsonSerializerOptions.PropertyNameCaseInsensitive = true; 
    });

builder.WebHost.UseUrls(
    "http://localhost:5276",
    "https://localhost:7043"
);

builder.Services.AddDbContext<StudentskaContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("STUDENTSKA")));

builder.Services.AddCors(options =>
{
    options.AddPolicy("VuePolicy", policy =>
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader());
});

builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseCors("VuePolicy");
app.UseAuthorization();
app.MapControllers();

app.Run();
