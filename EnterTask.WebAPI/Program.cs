
using EnterTask.DataAccess;
using EnterTask.Logic;
using EnterTask.WebAPI.DTOs;
using EnterTask.WebAPI.Mappers;
using EnterTask.WebAPI.Validation;
using FluentValidation;
using FluentValidation.AspNetCore;

namespace EnterTask.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddLogic(builder.Configuration);
            builder.Services.AddDataAccess(builder.Configuration);
            builder.Services.AddAutoMapper(typeof(MappingProfile));

            builder.Services.AddScoped<IValidator<EventDTO>, EventDTOValidator>();
            builder.Services.AddScoped<IValidator<ParticipantDTO>, ParticipantDTOValidator>();
            builder.Services.AddScoped<IValidator<RegistrationDTO>, RegistrationDTOValidator>();

            builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
