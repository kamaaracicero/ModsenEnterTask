using EnterTask.Data.DataEntities;
using EnterTask.Logic.Filter;
using EnterTask.Logic.Filter.Event;
using EnterTask.Logic.Repositories;
using EnterTask.Logic.Repositories.PaggingRepositories;
using EnterTask.Logic.Repositories.Related;
using EnterTask.Logic.Repositories.Related.Resolvers;
using EnterTask.Logic.Repositories.Tracking;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EnterTask.Logic
{
    internal static class DependencyInjection
    {
        public static IServiceCollection AddLogic(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddTransient<IFilter<Event>, EventFilter>();

            services.AddScoped<IRepository<EventChange>, EventChangeRepository>();
            services.AddScoped<IRepository<Event>, EventRepository>();
            services.AddScoped<IRepository<Participant>, ParticipantRepository>();
            services.AddScoped<IRepository<Registration>, RegistrationRepository>();

            services.AddScoped<IRelatedEntityResolver<Event, EventChange>, EventToEventChangeResolver>();
            services.AddScoped<IRelatedEntityResolver<Event, Participant>, EventToParticipantResolver>();
            services.AddScoped<IRelatedEntityResolver<Participant, Event>, ParticipantToEventResolver>();

            services.AddScoped<IRelatedRepository<Event>, EventRelatedRepository>();
            services.AddScoped<IRelatedRepository<Participant>, ParticipantRelatedRepository>();

            services.AddScoped<IPaggingRepository<Event>, EventPaggingRepository>();
            services.AddScoped<IPaggingRepository<Participant>, ParticipantPaggingRepository>();

            services.AddScoped<ITrackingRepository<Event, EventChange>, EventChangeTrackingRepository>();

            return services;
        }
    }
}
