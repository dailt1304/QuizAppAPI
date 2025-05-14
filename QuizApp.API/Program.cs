using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using QuizApp.Application.Interfaces;
using QuizApp.Application.Services;
using QuizApp.Domain.EF;
using QuizApp.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddDbContext<QuizAppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("QuizAppDB")));

// Register dependencies
builder.Services.AddScoped<IQuizRepository, QuizRepository>();
builder.Services.AddScoped<QuizService>();

// Add CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

// Add Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Quiz API",
        Version = "v1",
        Description = "API for managing quizzes, questions, answers, and results."
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseCors("AllowAll");
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Quiz API v1");
});

app.UseAuthorization();
app.MapControllers();

app.Run();