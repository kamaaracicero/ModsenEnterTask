using EnterTask.Data.DataEntities;
using EnterTask.DataAccess.DbContexts;
using EnterTask.DataAccess.RelatedEntityResolvers;
using EnterTask.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EnterTask.DataAccess
{
    internal static class DependencyInjection
    {
        public static IServiceCollection AddDataAccess(this IServiceCollection services,
            IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("Default");

            services.AddDbContext<MainDbContext>(options => {
                options.UseSqlServer(connectionString);
            });

            services.AddScoped<IRepository<EventChange>, EventChangeRepository>();
            services.AddScoped<IRepository<EventImage>, EventImageRepository>();
            services.AddScoped<IRepository<Event>, EventRepository>();
            services.AddScoped<IRepository<Participant>, ParticipantRepository>();
            services.AddScoped<IRepository<Person>, PersonRepository>();
            services.AddScoped<IRepository<Registration>, RegistrationRepository>();

            services.AddScoped<IRelatedEntityResolver<Event, Participant>, EventToParticipantResolver>();
            services.AddScoped<IRelatedEntityResolver<Participant, Event>, ParticipantToEventResolver>();
            services.AddScoped<IRelatedEntityResolver<Event, EventImage>, EventToEventImageResolver>();
            services.AddScoped<IRelatedEntityResolver<Event, EventChange>, EventToEventChangeResolver>();

            return services;
        }
    }
}
