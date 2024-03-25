using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using TodoApi.Database;
using TodoApi.Middleware;
using TodoApi.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.Configure<AppSettings>(
    builder.Configuration.GetSection("AppSettings"));
builder.Services.AddDbContext<DatabaseContext>(opt => opt
        .UseNpgsql(
        builder.Configuration
        .GetConnectionString
        ("PostgresDBConnection")));

builder.Services.AddScoped<ITodoItemService, TodoItemService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddSingleton<IUserServiceFactory, UserServiceFactory>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
    swagger =>
    {
        //This is to generate the Default UI of Swagger Documentation
        swagger.SwaggerDoc("v1", new OpenApiInfo
        {
            Version = "v1",
            Title = "TodoAPI",
            Description = ".NET 8 Web API"
        });
        // To Enable authorization using Swagger (JWT)
        swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
        {
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
        });
        swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
    }
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseAuthentication();

app.UseMiddleware<JwtMiddleware>();

app.MapControllers();

app.Run();
