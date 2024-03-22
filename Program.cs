using System.Configuration;
using Microsoft.EntityFrameworkCore;
using TodoApi.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using TodoApi.Database;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddDbContext<DatabaseContext>(opt => opt
        .UseNpgsql(
        builder.Configuration
        .GetConnectionString
        ("PostgresDBConnection")));
builder.Services.AddScoped<ITodoItemService, TodoItemService>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
