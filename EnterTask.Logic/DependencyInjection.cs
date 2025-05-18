using EnterTask.Data.DataEntities;
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
            services.AddScoped<IRelatedEntityResolver<Event, EventChange>, EventToEventChangeResolver>();
            services.AddScoped<IRelatedEntityResolver<Event, Participant>, EventToParticipantResolver>();
            services.AddScoped<IRelatedEntityResolver<Participant, Event>, ParticipantToEventResolver>();

            services.AddScoped<ITrackingRepository<Event, EventChange>, EventChangeTrackingRepository>();

            return services;
        }
    }
}
