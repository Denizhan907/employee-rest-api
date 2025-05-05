using PersonnelApi.Repositories;
using PersonnelApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();                        // <-- add controllers
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// register your in-memory repo:
builder.Services.AddSingleton<IEmployeeRepository, InMemoryEmployeeRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();                                     // <-- map controller routes
app.Run();
