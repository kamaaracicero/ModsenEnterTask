using EnterTask.Data.DataEntities;
using EnterTask.DataAccess.DbContexts;
using EnterTask.Logic.Search;
using Microsoft.EntityFrameworkCore;

namespace EnterTask.Logic.Repositories.Related.Resolvers
{
    internal class EventToParticipantResolver : IRelatedEntityResolver<Event, Participant>
    {
        private readonly MainDbContext _mainDbContext;

        public EventToParticipantResolver(MainDbContext dbContext)
        {
            _mainDbContext = dbContext;
        }

        public async Task AddRelatedAsync(Event entity, Participant related)
        {
            var link = new Registration(related.Id, entity.Id, DateOnly.FromDateTime(DateTime.Now));

            await _mainDbContext.Registrations.AddAsync(link);
            await _mainDbContext.SaveChangesAsync();
        }

        public async Task RemoveRelatedAsync(Event entity, Participant related)
        {
            var search = await _mainDbContext.Registrations.FirstOrDefaultAsync(r => r.EventId == entity.Id
                                                                                && r.ParticipantId == related.Id);

            if (search == null)
                throw new ArgumentException("Element to delete not found!");

            _mainDbContext.Registrations.Remove(search);
            await _mainDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Participant>> GetRelatedAsync(Event entity)
        {
            return await _mainDbContext.Registrations
                .Where(r => r.EventId == entity.Id)
                .Select(r => r.Participant)
                .ToListAsync();
        }
    }
}
