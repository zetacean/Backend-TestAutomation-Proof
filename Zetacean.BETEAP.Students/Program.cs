using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Zetacean.BETEAP.Students.Automappers;
using Zetacean.BETEAP.Students.DTOs;
using Zetacean.BETEAP.Students.Models;
using Zetacean.BETEAP.Students.Repository;
using Zetacean.BETEAP.Students.Services;
using Zetacean.BETEAP.Students.Validators;

namespace Zetacean.BETEAP.Students
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddKeyedScoped<
                ICommonService<StudentDto, StudentInsertDto, StudentUpdateDto>,
                StudentService
            >("studentService");

            // Repository
            builder.Services.AddScoped<IRepository<Student>, StudenRepository>();

            // Database/EF Core
            builder.Services.AddDbContextPool<StudentContext>(opt =>
                opt.UseNpgsql(builder.Configuration.GetConnectionString("StudentsCS"))
            );

            // Validators
            builder.Services.AddScoped<IValidator<StudentInsertDto>, StudentInsertValidator>();
            builder.Services.AddScoped<IValidator<StudentUpdateDto>, StudentUpdateValidator>();

            // Mappers
            builder.Services.AddAutoMapper(_ => { }, typeof(MappingProfile));

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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
        }
    }
}
