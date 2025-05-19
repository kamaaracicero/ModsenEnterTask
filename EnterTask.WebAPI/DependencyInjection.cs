using EnterTask.Application;
using EnterTask.DataAccess;
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
    public static class DependencyInjection
    {
        public static IServiceCollection AddWebServices(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
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

            services.AddAuthorization(options => {
                options.AddPolicy("AdminOnly", policy => policy.RequireRole("admin"));
                options.AddPolicy("AuthorizedOnly", policy => policy.RequireRole("admin", "user"));
                options.AddPolicy("HasCustomClaim", policy => policy.RequireClaim("custom_claim", "true"));
            });

            services.AddDataAccess(configuration);
            services.AddApplication(configuration);
            services.AddAutoMapper(typeof(MappingProfile));

            services.AddScoped<IValidator<EventDTO>, EventDTOValidator>();
            services.AddScoped<IValidator<ParticipantDTO>, ParticipantDTOValidator>();
            services.AddScoped<IValidator<RegistrationDTO>, RegistrationDTOValidator>();
            services.AddScoped<IValidator<EventImageDTO>, EventImageDTOValidator>();
            services.AddScoped<IValidator<PersonDTO>, PersonDTOValidator>();
            services.AddScoped<IValidator<LoginDTO>, LoginDTOValidator>();

            services.AddFluentValidationAutoValidation()
                    .AddFluentValidationClientsideAdapters();
            services.AddControllers();

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(options => {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "EnterTask API", Version = "v1" });

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
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

            return services;
        }
    }
}
