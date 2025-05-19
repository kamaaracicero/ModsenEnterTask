using EnterTask.Application.Infrastructure.Comparers;
using EnterTask.Application.Infrastructure.Converters;
using EnterTask.Application.Infrastructure.Security;
using EnterTask.Application.Services;
using EnterTask.Application.Services.Interfaces;
using EnterTask.Data.DataEntities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EnterTask.Application
{
    internal static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddTransient<IPasswordHasher, Pbkdf2PasswordHasher>();
            services.AddTransient<IConverter<PropertyChange, EventChange>, PropertyChangeToEventChangeConverter>();

            services.AddScoped<IEventChangeTrackingService, EventChangeTrackingService>();
            services.AddScoped<IEventImageService, EventImageService>();
            services.AddScoped<IEventService, EventService>();
            services.AddScoped<IParticipantService, ParticipantService>();
            services.AddScoped<IRegistrationService, RegistrationService>();
            services.AddScoped<IPersonService, PersonService>();

            return services;
        }
    }
}
