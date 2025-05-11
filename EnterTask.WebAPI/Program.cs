
using EnterTask.DataAccess;
using EnterTask.Logic;
using EnterTask.WebAPI.DTOs;
using EnterTask.WebAPI.Mappers;
using EnterTask.WebAPI.Security;
using EnterTask.WebAPI.Validation;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace EnterTask.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => {
                    options.TokenValidationParameters = new TokenValidationParameters {
                        ValidateIssuer = true,
                        ValidIssuer = AuthOptions.ISSUER,

                        ValidateAudience = true,
                        ValidAudience = AuthOptions.AUDIENCE,

                        ValidateLifetime = true,

                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),

                        ClockSkew = TimeSpan.Zero,
                    };
                });

            builder.Services.AddLogic(builder.Configuration);
            builder.Services.AddDataAccess(builder.Configuration);
            builder.Services.AddAutoMapper(typeof(MappingProfile));

            builder.Services.AddScoped<IValidator<EventDTO>, EventDTOValidator>();
            builder.Services.AddScoped<IValidator<ParticipantDTO>, ParticipantDTOValidator>();
            builder.Services.AddScoped<IValidator<RegistrationDTO>, RegistrationDTOValidator>();

            builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options => {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "EnterTask API", Version = "v1" });

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter JWT token in next format: Bearer {token}",
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement {
                    {
                        new OpenApiSecurityScheme {
                            Reference = new OpenApiReference {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer",
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });

            var app = builder.Build();

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
